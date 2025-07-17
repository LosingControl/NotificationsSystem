using FluentValidation;
using Infrastructure.Abstractions.Abstractions;
using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using NotificationService.API.Middlewares;
using NotificationService.Application.Behaviors;
using NotificationService.Application.Commands.Handlers;
using NotificationService.Application.MappingProfiles;
using NotificationService.Application.Queries.Validators;
using NotificationService.Application.Validations;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Caching;
using NotificationService.Infrastructure.Data;
using NotificationService.Infrastructure.Data.Repositories;
using StackExchange.Redis;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace NotificationService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var builderConfiguration = builder.Configuration;
            var logger = NLog.LogManager.Setup()
                .LoadConfigurationFromFile("nlog.config")
                .GetCurrentClassLogger();

            try
            {
                // Add services to the container.
                builder.Services.AddControllers()
                    .AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(option =>
                {
                    var filePath = AppContext.BaseDirectory;

                    var xmlPath = Path.Combine(filePath, "NotificationService.API.xml");
                    option.IncludeXmlComments(xmlPath);
                });

                builder.Services.Configure<Microsoft.AspNetCore.Http.Features.HttpResponseFeature>(options =>
                {
                    options.Headers["Content-Type"] = "text/html; charset=utf-8";
                });

                builder.Services.AddControllers().AddJsonOptions(options => {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.AddValidatorsFromAssembly(typeof(GetNotificationsByFilterQueryValidator).Assembly);

                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(builderConfiguration.GetConnectionString(nameof(AppDbContext)));
                });

                builder.Services.AddAutoMapper(cfg =>
                {
                    cfg.AddMaps(typeof(NotificationProfile).Assembly);
                });

                builder.Services.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssembly(typeof(CreateNotificationCommandHandler).Assembly));

                builder.Services.AddStackExchangeRedisCache(option =>
                {
                    option.Configuration = builderConfiguration["Redis:ConnectionString"];
                    option.InstanceName = "NotificationService_";
                });

                builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
                    ConnectionMultiplexer.Connect(
                        builderConfiguration["Redis:ConnectionString"]));

                builder.Services.AddSingleton<DistributedCacheEntryOptions>(option => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                        builderConfiguration.GetValue<int>("Redis:CacheSettings:DefaultAbsoluteExpirationMinutes")),
                    SlidingExpiration = TimeSpan.FromSeconds(
                        builderConfiguration.GetValue<int>("Redis:CacheSettings:DefaultSlidingExpirationSeconds"))
                });
                builder.Services.AddSingleton<NotificationCacheManager>(option => 
                    new NotificationCacheManager(
                            option.GetRequiredService<IConnectionMultiplexer>(), "NotificationService_"));

                builder.Services.AddScoped<DbContext>(provider =>
                    provider.GetRequiredService<AppDbContext>());

                builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
                builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

                builder.Services.AddScoped<INotificationCommandRepository>(provider =>
                {
                    var context = provider.GetRequiredService<AppDbContext>();
                    var cacheManager = provider.GetRequiredService<NotificationCacheManager>();
                    var distributedCache = provider.GetRequiredService<IDistributedCache>();
                    var cacheOptions = provider.GetRequiredService<DistributedCacheEntryOptions>();

                    var repository = new NotificationCommandRepository(context, distributedCache, cacheOptions);

                    repository.AddPostSaveAction(cacheManager.InvalidateCacheForNotificationsAsync);

                    return repository;
                });
                builder.Services.AddScoped<INotificationQueryRepository, NotificationQueryRepository>();

                builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var dbContext = services.GetRequiredService<AppDbContext>();
                        dbContext.Database.Migrate();
                        logger.Info("Миграции успешно применены");

                        if (!dbContext.Users.Any())
                        {
                            dbContext.Users.Add(new User
                            (
                                Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                                "test@test.test",
                                null
                            ));
                            dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Ошибка при применении миграций");
                        throw;
                    }
                }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseMiddleware<ExceptionHandlingMiddleware>();

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Lifetime.ApplicationStopped.Register(() =>
                {
                    NLog.LogManager.Shutdown();
                });

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application crashed!");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}

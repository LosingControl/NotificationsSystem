using FluentValidation;
using Infrastructure.Abstractions.Abstractions;
using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using NotificationService.API.Middlewares;
using NotificationService.Application.Behaviors;
using NotificationService.Application.Commands.Handlers;
using NotificationService.Application.Queries.Validators;
using NotificationService.Application.Validations;
using NotificationService.Infrastructure.Data;
using NotificationService.Infrastructure.Data.Repositories;
using System.Text.Json.Serialization;

namespace NotificationService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var dbConfiguration = builder.Configuration;
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

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.AddValidatorsFromAssembly(typeof(GetNotificationsByFilterQueryValidator).Assembly);

                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(dbConfiguration.GetConnectionString(nameof(AppDbContext)));
                });
                builder.Services.AddScoped<DbContext>(provider =>
                    provider.GetRequiredService<AppDbContext>());

                builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
                builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

                builder.Services.AddScoped<INotificationCommandRepository, NotificationCommandRepository>();
                builder.Services.AddScoped<INotificationQueryRepository, NotificationQueryRepository>();

                builder.Services.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssembly(typeof(CreateNotificationCommandHandler).Assembly));

                builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

                var app = builder.Build();

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

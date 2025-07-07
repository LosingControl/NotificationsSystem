using Infrastructure.Abstractions.Abstractions;
using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Commands.Handlers;
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

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(dbConfiguration.GetConnectionString(nameof(AppDbContext)));
            });

            builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            builder.Services.AddScoped<INotificationCommandRepository, NotificationCommandRepository>();
            builder.Services.AddScoped<INotificationQueryRepository, NotificationQueryRepository>();

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateNotificationCommandHandler).Assembly));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

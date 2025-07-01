using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Data
{
    /// <summary>
    /// Контекст базы данных приложения
    /// </summary>
    /// <remarks>
    /// Основной класс для взаимодействия с базой данных через Entity Framework Core.
    /// <para><b>Таблицы базы данных:</b></para>
    /// <list type="number">
    ///   <item><see cref="Notifications"/> - уведомления системы</item>
    ///   <item><see cref="Users"/> - пользователи системы</item>
    ///   <item><see cref="NotificationArchives"/> - архив уведомлений</item>
    ///   <item><see cref="SmtpServers"/> - SMTP-серверы для отправки email</item>
    /// </list>
    /// </remarks>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) {}

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NotificationArchive> NotificationArchives { get; set; }
        public DbSet<SmtpServer> SmtpServers { get; set; }

        /// <summary>
        /// Конфигурирует модель базы данных
        /// </summary>
        /// <param name="modelBuilder">Построитель модели Entity Framework</param>
        /// <remarks>
        /// Применяет все конфигурации сущностей:
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureNotifications());
            modelBuilder.ApplyConfiguration(new ConfigureArchives());
            modelBuilder.ApplyConfiguration(new ConfigureUsers());
            modelBuilder.ApplyConfiguration(new ConfigureSmtpServers());

            base.OnModelCreating(modelBuilder);
        }
    }
}

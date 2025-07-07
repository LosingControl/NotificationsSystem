using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Data.Configurations
{
    /// <summary>Конфигурация таблицы уведомлений в БД</summary>
    public class ConfigureNotifications : IEntityTypeConfiguration<Notification>
    {
        public ConfigureNotifications()
        { }

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title).HasMaxLength(200);

            builder.Property(n => n.Message).HasMaxLength(2000);

            builder.Property(n => n.Status)
                .HasConversion<string>()
                .HasDefaultValue(NotificationStatus.Pending);

            builder.Property(n => n.CreatedAt)
                .HasDefaultValueSql("NOW()");

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
        }
    }
}

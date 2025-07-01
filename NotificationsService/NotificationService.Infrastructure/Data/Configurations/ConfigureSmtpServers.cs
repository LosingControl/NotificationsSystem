using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Data.Configurations
{
    /// <summary>Конфигурация таблицы SMTP-серверов в БД</summary>
    public class ConfigureSmtpServers : IEntityTypeConfiguration<SmtpServer>
    {
        public void Configure(EntityTypeBuilder<SmtpServer> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Config)
                .HasColumnType("jsonb");

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);
        }
    }
}

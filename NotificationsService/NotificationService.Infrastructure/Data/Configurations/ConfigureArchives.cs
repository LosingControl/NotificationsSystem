using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Data.Configurations
{
    /// <summary>Конфигурация таблицы архивных уведомлений в БД</summary>
    public class ConfigureArchives : IEntityTypeConfiguration<NotificationArchive>
    {
        public ConfigureArchives()
        { }

        public void Configure(EntityTypeBuilder<NotificationArchive> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.OriginalJson)
                .HasColumnType("jsonb");

            builder.Property(a => a.ArchivedAt)
                .HasDefaultValueSql("NOW()");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Data.Configurations
{
    /// <summary>Конфигурация таблицы пользователей в БД</summary>
    public class ConfigureUsers : IEntityTypeConfiguration<User>
    {
        public ConfigureUsers()
        { }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasMaxLength(100);
        }
    }
}

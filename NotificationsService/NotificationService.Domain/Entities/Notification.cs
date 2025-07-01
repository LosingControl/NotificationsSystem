using NotificationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    /// <summary>
    /// Модель данных уведомления в системе
    /// </summary>
    /// <remarks>
    /// Содержит информацию об уведомлении.
    /// Соответствует таблице уведомлений в базе данных.
    /// </remarks>
    public class Notification
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
        public NotificationType Type { get; set; }
        public NotificationPriority Priority { get; set; } = NotificationPriority.Medium;
        public DateTime CreatedAt { get; set; }
        public DateTime? SentAt { get; set; }
        public int RetryCount { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

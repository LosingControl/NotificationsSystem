using NotificationService.Domain.Enum;

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
        /// <summary>
        /// Уникальный идентификатор уведомления.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок уведомления. Может отсутствовать (null).
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Основной текст уведомления. По умолчанию - пустая строка.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Текущий статус уведомления. По умолчанию - "Pending" (В ожидании).
        /// </summary>
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

        /// <summary>
        /// Тип уведомления (определяет способ доставки).
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Приоритет уведомления. По умолчанию - "Medium" (Средний).
        /// </summary>
        public NotificationPriority Priority { get; set; } = NotificationPriority.Medium;

        /// <summary>
        /// Дата и время создания уведомления (в UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата и время отправки уведомления (в UTC). Null, если еще не отправлено.
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// Количество попыток отправки уведомления.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Идентификатор пользователя, связанного с уведомлением.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Навигационное свойство для пользователя, связанного с уведомлением.
        /// </summary>
        public User User { get; set; }

        public Notification(
            Guid id,
            string? title,
            string message,
            NotificationStatus status,
            NotificationType type,
            NotificationPriority priority,
            DateTime createdAt,
            Guid userId)
        {
            Id = id;
            Title = title;
            Message = message;
            Status = status;
            Type = type;
            Priority = priority;
            CreatedAt = createdAt;
            UserId = userId;
        }
    }
}

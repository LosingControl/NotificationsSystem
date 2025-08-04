using NotificationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Common.DTO_s
{
    /// <summary>
    /// Data Transfer Object (DTO) для представления уведомления.
    /// </summary>
    public class NotificationDTO
    {
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

        public NotificationDTO()
        { }

        public NotificationDTO(
            string? title, 
            string message, 
            NotificationStatus status, 
            NotificationType type, 
            NotificationPriority priority, 
            DateTime createdAt, 
            DateTime? sentAt, 
            int retryCount, 
            Guid userId)
        {
            Title = title;
            Message = message;
            Status = status;
            Type = type;
            Priority = priority;
            CreatedAt = createdAt;
            SentAt = sentAt;
            RetryCount = retryCount;
            UserId = userId;
        }
    }
}

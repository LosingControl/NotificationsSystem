using MediatR;
using NotificationService.Domain.Enum;

namespace NotificationService.Application.Commands
{
    /// <summary>
    /// Команда для создания нового уведомления
    /// </summary>
    /// <remarks>
    /// Содержит все необходимые данные для создания уведомления в системе.
    /// </remarks>
    public record CreateNotificationCommand : IRequest<bool>
    {
        /// <summary>
        /// Заголовок уведомления (опционально)
        /// </summary>
        /// <remarks>
        /// Может использоваться для краткого описания уведомления.
        /// </remarks>
        public string? Title { get; set; }

        /// <summary>
        /// Основной текст уведомления
        /// </summary>
        /// <remarks>
        /// Обязательное поле
        /// </remarks>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Статус уведомления при создании
        /// </summary>
        public NotificationStatus Status { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        /// <remarks>
        /// Определяет способ доставки уведомления (Email, Push и т.д.)
        /// </remarks>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Приоритет уведомления
        /// </summary>
        /// <remarks>
        /// Влияет на порядок обработки уведомлений.
        /// </remarks>
        public NotificationPriority Priority { get; set; }

        /// <summary>
        /// Дата и время создания уведомления
        /// </summary>
        /// <remarks>
        /// Обычно устанавливается текущая дата/время (UTC).
        /// </remarks>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Идентификатор пользователя-получателя
        /// </summary>
        /// <remarks>
        /// Должен соответствовать существующему пользователю в системе.
        /// </remarks>
        public Guid UserId { get; set; }
    }
}

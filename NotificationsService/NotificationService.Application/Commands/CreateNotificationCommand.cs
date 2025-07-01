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
        public string? Title { get; set; }
        public string Message { get; set; } = string.Empty;
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }
        public NotificationPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}

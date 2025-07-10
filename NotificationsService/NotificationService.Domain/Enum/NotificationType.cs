using System;

namespace NotificationService.Domain.Enum
{
    /// <summary>
    /// Тип уведомления
    /// </summary>
    /// <remarks>
    /// Определяет канал доставки уведомления пользователю.
    /// </remarks>
    public enum NotificationType
    {
        /// <summary>
        /// Тип не определен
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Электронная почта (SMTP)
        /// </summary>
        Email = 1,

        /// <summary>
        /// Push-уведомление
        /// </summary>
        Push = 2
    }
}

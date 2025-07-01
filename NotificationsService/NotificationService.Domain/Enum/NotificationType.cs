using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        Email,
        Push
    }
}

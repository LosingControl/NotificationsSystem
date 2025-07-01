using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotificationService.Domain.Enum
{
    /// <summary>
    /// Статус уведомления в системе доставки
    /// </summary>
    /// <remarks>
    /// Отслеживает текущее состояние уведомления в процессе обработки.
    /// </remarks>
    public enum NotificationStatus
    {
        Pending,
        Sent,
        Failed,
        Retrying
    }
}

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
        /// <summary>
        /// Статус не определен
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Ожидает отправки (начальное состояние)
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Успешно отправлено
        /// </summary>
        Sent = 2,

        /// <summary>
        /// Ошибка отправки
        /// </summary>
        Failed = 3,

        /// <summary>
        /// Повторная попытка отправки (временный статус)
        /// </summary>
        Retrying = 4
    }
}

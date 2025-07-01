using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Enum
{
    /// <summary>
    /// Приоритет уведомления
    /// </summary>
    /// <remarks>
    /// Определяет срочность обработки и доставки уведомления.
    /// Влияет на порядок обработки в очереди.
    /// </remarks>
    public enum NotificationPriority
    {
        Low,
        Medium,
        High
    }
}

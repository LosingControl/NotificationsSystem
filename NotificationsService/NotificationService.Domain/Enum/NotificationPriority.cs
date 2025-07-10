using System;

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
        /// <summary>
        /// Приоритет не задан
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Низкий приоритет - обрабатывается в последнюю очередь (используется по умолчанию)
        /// </summary>
        Low = 1,

        /// <summary>
        /// Средний приоритет - стандартная обработка
        /// </summary>
        Medium = 2,

        /// <summary>
        /// Высокий приоритет - обрабатывается в первую очередь
        /// </summary>
        High = 3
    }
}

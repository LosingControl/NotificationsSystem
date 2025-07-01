using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    /// <summary>
    /// Модель архивной копии уведомления
    /// </summary>
    /// <remarks>
    /// Используется для хранения данных об устаревших уведомлениях.
    /// Содержит полную JSON-копию оригинального уведомления на момент архивации.
    /// </remarks>
    public class NotificationArchive
    {
        public Guid Id { get; set; }
        public required Notification OriginalJson { get; set; }
        public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;
    }
}

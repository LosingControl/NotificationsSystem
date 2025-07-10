using System;

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
        /// <summary>
        /// Уникальный идентификатор архивной записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Оригинальное уведомление в сериализованном JSON-формате.
        /// </summary>
        /// <remarks>
        /// Содержит данные уведомления на момент архивации.
        /// </remarks>
        public required Notification OriginalJson { get; set; }

        /// <summary>
        /// Дата и время архивации уведомления (в UTC).
        /// </summary>
        /// <remarks>
        /// По умолчанию устанавливается текущая дата/время при создании записи.
        /// </remarks>
        public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="originalJson">Данные в JSON-формате</param>
        /// <param name="archivedAt">Дата и время архивации</param>
        public NotificationArchive(Guid id, Notification originalJson, DateTime archivedAt)
        {
            Id = id;
            OriginalJson = originalJson;
            ArchivedAt = archivedAt;
        }
    }
}

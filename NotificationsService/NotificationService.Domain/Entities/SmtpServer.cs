using System;

namespace NotificationService.Domain.Entities
{
    /// <summary>
    /// Конфигурация SMTP-сервера для системы отправки электронной почты
    /// </summary>
    /// <remarks>
    /// Представляет настраиваемый SMTP-сервер с ограничениями и приоритетами отправки.
    /// <para><b>Особенности работы:</b></para>
    /// <list type="number">
    ///   <item>Ограничение количества отправляемых писем в день</item>
    ///   <item>Приоритетность использования серверов</item>
    ///   <item>Возможность временного отключения сервера</item>
    /// </list>
    /// </remarks>
    public class SmtpServer
    {
        /// <summary>
        /// Уникальный идентификатор сервера
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название сервера (для идентификации в системе)
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Приоритет сервера (чем меньше число, тем выше приоритет)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Флаг активности сервера
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Дневной лимит отправляемых писем
        /// </summary>
        /// <remarks>
        /// По умолчанию: 1000 писем в день
        /// </remarks>
        public int DailyLimit { get; set; } = 1000;

        /// <summary>
        /// Конфигурация подключения к SMTP серверу
        /// </summary>
        /// <remarks>
        /// Обязательный параметр
        /// </remarks>
        public required SmtpConfig Config { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Название сервера</param>
        /// <param name="priority">Приоритет сервера</param>
        /// <param name="isActive">Флаг активности</param>
        /// <param name="dailyLimit">Лимит отправляемых писем</param>
        /// <param name="config">Конфигурация подключения</param>
        public SmtpServer(Guid id, string name, int priority, bool isActive, int dailyLimit, SmtpConfig config)
        {
            Id = id;
            Name = name;
            Priority = priority;
            IsActive = isActive;
            DailyLimit = dailyLimit;
            Config = config;
        }
    }
}

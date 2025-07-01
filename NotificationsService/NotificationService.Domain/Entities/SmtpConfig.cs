using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{
    /// <summary>
    /// Конфигурация SMTP-сервера для отправки электронной почты
    /// </summary>
    /// <remarks>
    /// Содержит параметры подключения к SMTP-серверу.
    /// Используется для настройки сервисов отправки email-уведомлений.
    /// </remarks>
    public class SmtpConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

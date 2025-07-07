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
        /// <summary>
        /// Адрес SMTP сервера
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Порт SMTP сервера
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Имя пользователя для аутентификации
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Пароль для аутентификации
        /// </summary>
        /// <remarks>
        /// Значение должно быть защищено при хранении
        /// </remarks>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="host">Адрес SMTP</param>
        /// <param name="port">Порт SMTP</param>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль для аутентификации</param>
        public SmtpConfig(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
        }
    }
}

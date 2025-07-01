using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public int DailyLimit { get; set; } = 1000;
        public required SmtpConfig Config { get; set; }
    }
}

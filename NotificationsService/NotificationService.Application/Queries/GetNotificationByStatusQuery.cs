using MediatR;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Queries
{
    /// <summary>
    /// Запрос для получения уведомлений по статусу
    /// </summary>
    /// <remarks>
    /// Содержит все необходимые данные для поиска уведомлений в системе.
    /// </remarks>
    public record GetNotificationByStatusQuery(NotificationStatus Status) : IRequest<List<Notification>>
    { }
}

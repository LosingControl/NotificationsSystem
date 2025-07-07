using MediatR;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Queries
{
    /// <summary>
    /// Запрос для получения уведомлений пользователя по его индентификатору
    /// </summary>
    /// <remarks>
    /// Содержит все необходимые данные для поиска уведомлений пользователя в системе.
    /// </remarks>
    public record GetNotificationByUserIdQuery(Guid? Id) : IRequest<List<Notification>>
    { }
}

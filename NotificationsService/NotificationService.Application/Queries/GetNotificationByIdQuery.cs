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
    /// Запрос для получения уведомления по его индентификатору
    /// </summary>
    /// <remarks>
    /// Содержит все необходимые данные для поиска уведомления в системе.
    /// </remarks>
    public record GetNotificationByIdQuery(Guid? Id) : IRequest<Notification?>
    { }
}

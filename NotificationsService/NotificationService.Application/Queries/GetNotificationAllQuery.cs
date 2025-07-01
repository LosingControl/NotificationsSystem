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
    /// Запрос для получения всех уведомлений из базы
    /// </summary>
    public record GetNotificationAllQuery : IRequest<List<Notification>>
    { }
}

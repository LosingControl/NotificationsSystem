using MediatR;
using NotificationService.Application.Common.DTO_s;
using NotificationService.Application.Common.Pagination;
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
    /// Запрос для получения уведомлений с филтрацией
    /// </summary>
    /// <remarks>
    /// Содержит все необходимые данные для поиска уведомлений.
    /// </remarks>
    public record GetNotificationsByFilterQuery(NotificationFiltersDTO Filters) : IRequest<PaginatedListDTO<Notification>>
    { }
}

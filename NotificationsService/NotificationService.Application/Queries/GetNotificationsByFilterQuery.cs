using MediatR;
using NotificationService.Application.Common.DTO_s;
using NotificationService.Application.Common.Pagination;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Queries
{
    /// <summary>
    /// Запрос для получения уведомлений с филтрацией
    /// </summary>
    /// <remarks>
    /// Содержит все необходимые данные для поиска уведомлений.
    /// </remarks>
    public record GetNotificationsByFilterQuery(NotificationFiltersDTO Filters) : IRequest<PaginatedListDTO<NotificationDTO>>
    { }
}

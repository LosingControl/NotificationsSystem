using Infrastructure.Abstractions.Abstractions;
using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using MediatR;
using NotificationService.Application.Common.Pagination;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение уведомлений с фильтрацией и пагинацией
    /// </summary>
    /// <remarks>
    /// Реализует логику получения уведомлений с применением фильтров и пагинации.
    /// <para><b>Особенности работы:</b></para>
    /// <list type="number">
    ///   <item>Поддерживает фильтрацию по статусу, ID уведомления и ID пользователя</item>
    ///   <item>Возвращает результат в формате пагинированного списка</item>
    ///   <item>При отсутствии фильтров возвращает все доступные уведомления</item>
    ///   <item>Включает общее количество элементов для корректной пагинации</item>
    /// </list>
    /// </remarks>
    public class GetNotificationsByFilterQueryHandler
        : IRequestHandler<GetNotificationsByFilterQuery, PaginatedListDTO<Notification>>
    {
        private readonly IQueryRepository<Notification> _baseRepository;
        private readonly INotificationQueryRepository _notificationQuerySpecific;

        public GetNotificationsByFilterQueryHandler(
            INotificationQueryRepository repository,
            IQueryRepository<Notification> queryRepository)
        {
            _notificationQuerySpecific = repository;
            _baseRepository = queryRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение уведомлений с фильтрами
        /// </summary>
        /// <param name="request">Запрос, содержащий параметры фильтрации и пагинации</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Пагинированный список уведомлений в формате <see cref="PaginatedListDTO{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Генерируется при передаче null запроса</exception>
        /// <exception cref="InvalidOperationException">Генерируется при ошибках выполнения запроса</exception>
        public async Task<PaginatedListDTO<Notification>> Handle(
            GetNotificationsByFilterQuery request, 
            CancellationToken cancellationToken)
        {
            Expression<Func<Notification, bool>> filters = n =>
            (request.Filters.Status == null || n.Status == request.Filters.Status) &&
            (request.Filters.Id == null || n.Id == request.Filters.Id) &&
            (request.Filters.UserId == null || n.UserId == request.Filters.UserId);

            try
            {
                var items = _notificationQuerySpecific.GetAllPaginatedFilteredAsync(
                filters,
                request.Filters.PageNumber,
                request.Filters.PageSize,
                cancellationToken);

                var totalCount = await _notificationQuerySpecific.GetTotalCountWithFiltersAsync(filters, cancellationToken);

                return new PaginatedListDTO<Notification>(
                items,
                totalCount,
                request.Filters.PageNumber,
                request.Filters.PageSize);
            }
            catch (Exception ex)
            {
                //логирование
                throw new InvalidOperationException("Ошибка при получении уведомлений", ex);
            }
        }
    }
}

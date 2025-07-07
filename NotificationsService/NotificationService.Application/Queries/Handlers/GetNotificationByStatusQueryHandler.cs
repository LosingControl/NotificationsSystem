using Infrastructure.Abstractions.Abstractions;
using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using MediatR;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение уведомлений по статусу
    /// </summary>
    /// <remarks>
    /// Реализует логику получения списка уведомлений из системы по указанному статусу.
    /// Возвращает только активные записи (без удаленных/архивных)
    /// </remarks>
    public class GetNotificationByStatusQueryHandler
        : IRequestHandler<GetNotificationByStatusQuery, List<Notification>>
    {
        private readonly IQueryRepository<Notification> _baseRepository;
        private readonly INotificationQueryRepository _notificationQuerySpecific;

        public GetNotificationByStatusQueryHandler(
            INotificationQueryRepository repositorySpecific,
            IQueryRepository<Notification> queryRepository)
        {
            _notificationQuerySpecific = repositorySpecific;
            _baseRepository = queryRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение уведомлений по статусу
        /// </summary>
        /// <param name="request">Запрос, содержащий статус для фильтрации</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Список уведомлений с указанным статусом.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        /// <exception cref="ArgumentNullException">Генерируется при передаче null запроса</exception>
        public async Task<List<Notification>> Handle(GetNotificationByStatusQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _notificationQuerySpecific.GetByStatusAsync(request.Status, cancellationToken);
        }
    }
}

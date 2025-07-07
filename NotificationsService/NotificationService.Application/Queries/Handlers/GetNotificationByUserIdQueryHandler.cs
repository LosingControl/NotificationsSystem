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
    /// Обработчик запроса на получение уведомлений по идентификатору пользователя
    /// </summary>
    /// <remarks>
    /// Реализует логику получения списка уведомлений для конкретного пользователя.
    /// <para><b>Особенности работы:</b></para>
    /// <list type="number">
    ///   <item>Результат отсортирован по дате создания (от новых к старым)</item>
    ///   <item>Возвращает пустой список, если уведомлений не найдено</item>
    ///   <item>Не включает удаленные/архивные уведомления</item>
    /// </list>
    /// </remarks>
    public class GetNotificationByUserIdQueryHandler
        : IRequestHandler<GetNotificationByUserIdQuery, List<Notification>>
    {
        private readonly IQueryRepository<Notification> _baseRepository;
        private readonly INotificationQueryRepository _notificationQuerySpecific;

        public GetNotificationByUserIdQueryHandler(
            INotificationQueryRepository repository,
            IQueryRepository<Notification> queryRepository)
        {
            _notificationQuerySpecific = repository;
            _baseRepository = queryRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение уведомлений пользователя
        /// </summary>
        /// <param name="request">Запрос, содержащий идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Список уведомлений для указанного пользователя.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        /// <exception cref="ArgumentNullException">Генерируется при передаче null запроса</exception>
        /// <exception cref="ArgumentException">Генерируется при пустом Guid пользователя</exception>
        public async Task<List<Notification>> Handle(GetNotificationByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("ID пользователя не может быть пустым!", nameof(request.Id));
            }

            return await _notificationQuerySpecific.GetByUserIdAsync(request.Id, cancellationToken);
        }
    }
}

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
    /// Обработчик запроса на получение всех уведомлений
    /// </summary>
    /// <remarks>
    /// Реализует логику получения полного списка уведомлений из системы.
    /// <para><b>Особенности работы:</b></para>
    /// <list type="number">
    ///   <item>Возвращает все уведомления без фильтрации</item>
    ///   <item>Возвращает пустой список, если уведомлений нет</item>
    /// </list>
    /// </remarks>
    public class GetNotificationAllQueryHandler
        : IRequestHandler<GetNotificationAllQuery, List<Notification>>
    {
        private readonly IQueryRepository<Notification> _baseRepository;
        private readonly INotificationQueryRepository _notificationQuerySpecific;

        public GetNotificationAllQueryHandler(
            INotificationQueryRepository repositorySpecific,
            IQueryRepository<Notification> queryRepository)
        {
            _notificationQuerySpecific = repositorySpecific;
            _baseRepository = queryRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение всех уведомлений
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Список всех уведомлений в системе.
        /// Возвращает пустой список, если уведомлений нет.
        /// </returns>
        /// <exception cref="ArgumentNullException">Если запрос не указан</exception>
        public async Task<List<Notification>> Handle(GetNotificationAllQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _baseRepository.GetAllAsync();
        }
    }
}

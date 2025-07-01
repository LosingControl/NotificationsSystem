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
    /// Обработчик запроса на получение уведомления по идентификатору
    /// </summary>
    /// <remarks>
    /// Реализует логику получения данных уведомления из системы по его уникальному идентификатору.
    /// </remarks>
    public class GetNotificationByIdQueryHandler
        : IRequestHandler<GetNotificationByIdQuery, Notification?>
    {

        private readonly INotificationQueryRepository _repository;

        public GetNotificationByIdQueryHandler(INotificationQueryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение уведомления
        /// </summary>
        /// <param name="request">Запрос, содержащий идентификатор уведомления</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Возвращает <see cref="Notification"/> если уведомление найдено,
        /// или <c>null</c> если уведомление с указанным ID не существует
        /// </returns>
        /// <exception cref="ArgumentNullException">Возникает при передаче null запроса</exception>
        public async Task<Notification?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

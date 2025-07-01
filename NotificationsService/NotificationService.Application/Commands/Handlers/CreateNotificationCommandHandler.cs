using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using MediatR;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Commands.Handlers
{
    /// <summary>
    /// Обработчик запросов создания уведомлений
    /// </summary>
    /// <remarks>
    /// Создает новое уведомление на основе переданных данных и сохраняет его в репозитории.
    /// <para><b>Логика обработки:</b></para>
    /// <list type="number">
    ///   <item>Генерирует новый уникальный идентификатор (Guid) для уведомления</item>
    ///   <item>Создает объект Notification на основе данных из команды</item>
    ///   <item>Сохраняет уведомление через репозиторий</item>
    /// </list>
    /// </remarks>
    public class CreateNotificationCommandHandler
        : IRequestHandler<CreateNotificationCommand, bool>
    {
        private readonly INotificationCommandRepository _repository;

        public CreateNotificationCommandHandler(INotificationCommandRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Обрабатывает команду создания уведомления
        /// </summary>
        /// <param name="request">Команда с данными для создания уведомления</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Возвращает <c>true</c>, если уведомление было успешно создано и сохранено.
        /// В случае ошибки возвращает <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Если переданная команда равна null</exception>
        public async Task<bool> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Message = request.Message,
                Status = request.Status,
                Type = request.Type,
                Priority = request.Priority,
                CreatedAt = request.CreatedAt,
                UserId = request.UserId
            };

            return await _repository.AddAsync(notification);
        }
    }
}

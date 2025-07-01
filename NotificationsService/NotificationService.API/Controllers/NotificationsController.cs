using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Commands;
using NotificationService.Application.Queries;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;

namespace NotificationService.API.Controllers
{
    /// <summary>
    /// Точка входа запросов
    /// </summary>
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mediator"></param>
        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание уведомления
        /// </summary>
        /// <param name="command">Подробные сведения об уведомлении</param>
        /// <returns>Возвращает значение true, если уведомление было успешно создано</returns>
        [HttpPost]
        public async Task<bool> CreateNotification([FromBody] CreateNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Получения уведомления по Id
        /// </summary>
        /// <param name="id">Id уведомления</param>
        /// <returns>Уведомление</returns>
        [HttpGet("id/{id:guid}")]
        public async Task<Notification?> GetNotificationById(Guid id)
        {
            return await _mediator.Send(new GetNotificationByIdQuery(id));
        }

        /// <summary>
        /// Получение списка уведомлений с определённым статусом
        /// </summary>
        /// <param name="status">Статус уведомления.
        /// Доступные значения: 
        /// <br/>- Pending (Ожидание)
        /// <br/>- Sent (Отправлено)
        /// <br/>- Failed (Ошибка отправки)
        /// <br/>- Retrying (Повторная отправка)
        ///</param>
        /// <returns>Список уведомлений</returns>
        [HttpGet("status/{status}")]
        public async Task<List<Notification>> GetNotificationByStatus(NotificationStatus status)
        {
            return await _mediator.Send(new GetNotificationByStatusQuery(status));
        }

        /// <summary>
        /// Получение списка уведомлений пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Список уведомлений</returns>
        [HttpGet("userId/{userId}")]
        public async Task<List<Notification>> GetNotificationByUserId(Guid userId)
        {
            return await _mediator.Send(new GetNotificationByUserIdQuery(userId));
        }

        /// <summary>
        /// Получение полного списка уведомлений
        /// </summary>
        /// <returns>Список всех уведомлений</returns>
        [HttpGet("allnotifications")]
        public async Task<List<Notification>> GetNotificationsAll()
        {
            return await _mediator.Send(new GetNotificationAllQuery());
        }
    }
}

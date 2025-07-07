using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Commands;
using NotificationService.Application.Common.DTO_s;
using NotificationService.Application.Common.Pagination;
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
        /// <param name="mediator">Медиатор для обработки CQRS-запросов</param>
        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание уведомления
        /// </summary>
        /// <param name="command">Подробные сведения об уведомлении</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <response code="200">Уведомление успешно создано (true) или не создано (false)</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="500">Ошибка сервера при обработке запроса</response>
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<bool> CreateNotification(
            [FromBody] CreateNotificationCommand command,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Получает список уведомлений с возможностью фильтрации и пагинации
        /// </summary>
        /// <remarks>
        /// Позволяет получить уведомления с применением фильтров по статусу, идентификатору и пользователю.
        /// </remarks>
        /// <param name="filters">Параметры фильтрации и пагинации</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// <response code="200">Успешно возвращен список уведомлений</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="500">Ошибка сервера при обработке запроса</response>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedListDTO<Notification>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedListDTO<Notification>>> GetNotifications(
            [FromQuery] NotificationFiltersDTO filters,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetNotificationsByFilterQuery(filters), cancellationToken);
        }

        /*/// <summary>
        /// Получения уведомления по Id
        /// </summary>
        /// <param name="id">Id уведомления</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Уведомление</returns>
        [HttpGet("id/{id:guid}")]
        public async Task<Notification?> GetNotificationById(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetNotificationByIdQuery(id), cancellationToken);
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
        /// <param name="cancellationToken"></param>
        /// <returns>Список уведомлений</returns>
        [HttpGet("status/{status}")]
        public async Task<List<Notification>> GetNotificationByStatus(NotificationStatus status, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetNotificationByStatusQuery(status), cancellationToken);
        }

        /// <summary>
        /// Получение списка уведомлений пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Список уведомлений</returns>
        [HttpGet("userId/{userId}")]
        public async Task<List<Notification>> GetNotificationByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetNotificationByUserIdQuery(userId), cancellationToken);
        }

        /// <summary>
        /// Получение полного списка уведомлений
        /// </summary>
        /// <returns>Список всех уведомлений</returns>
        [HttpGet("allnotifications")]
        public async Task<List<Notification>> GetNotificationsAll()
        {
            return await _mediator.Send(new GetNotificationAllQuery());
        }*/
    }
}

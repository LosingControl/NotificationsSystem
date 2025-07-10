using FluentValidation;
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
        private readonly ILogger<NotificationsController> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mediator">Медиатор для обработки CQRS-запросов</param>
        /// <param name="logger">Логгер ошибок</param>
        public NotificationsController(IMediator mediator, ILogger<NotificationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            _logger.LogInformation("Создание уведомления");
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Получает список уведомлений с возможностью фильтрации и пагинации
        /// </summary>
        /// <remarks>
        /// Позволяет получить уведомления с применением фильтров по статусу, идентификатору и пользователю.
        /// <para>Формат ответа:</para>
        /// <para>
        /// Возвращает объект с:
        /// </para>
        /// <list type="number">
        ///   <item>Списком уведомлений на текущей странице</item>
        ///   <item>Общим количеством уведомлений</item>
        ///   <item>Информацией о пагинации</item>
        /// </list>
        /// </remarks>
        /// <param name="filters">
        /// Параметры фильтрации и пагинации:
        /// <list type="number">
        ///   <item>Status - фильтр по статусу уведомления</item>
        ///   <item>Id - фильтр по ID уведомления</item>
        ///   <item>UserId - фильтр по ID пользователя</item>
        ///   <item>PageNumber - номер страницы (по умолчанию 1)</item>
        ///   <item>PageSize - количество элементов на странице (по умолчанию 20)</item>
        /// </list>
        /// </param>
        /// <param name="validator">Валидатор</param>
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
    }
}

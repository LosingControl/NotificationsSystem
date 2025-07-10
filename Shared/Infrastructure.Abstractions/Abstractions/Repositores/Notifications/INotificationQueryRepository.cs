using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using System.Linq.Expressions;

namespace Infrastructure.Abstractions.Abstractions.Repositores.Notifications
{
    /// <summary>
    /// Интерфейс для специфичных запросов, свойственных только для уведомлений
    /// </summary>
    public interface INotificationQueryRepository
    {
        /// <summary>
        /// Получает уведомления по статусу
        /// </summary>
        /// <param name="status">Статус уведомления</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список уведомлений с указанным статусом</returns>
        public Task<List<Notification>> GetByStatusAsync(NotificationStatus? status, CancellationToken cancellationToken);

        /// <summary>
        /// Получает уведомления по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список уведомлений пользователя</returns>
        public Task<List<Notification>> GetByUserIdAsync(Guid? id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает пагинированный список уведомлений с фильтрацией
        /// </summary>
        /// <param name="filters">Условия фильтрации</param>
        /// <param name="pageNumber">Номер страницы (начиная с 1)</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Асинхронный перечислитель уведомлений</returns>
        public IAsyncEnumerable<Notification> GetAllPaginatedFilteredAsync(
            Expression<Func<Notification, bool>> filters,
            int pageNumber, 
            int pageSize,
            CancellationToken cancellationToken);

        /// <summary>
        /// Получает общее количество уведомлений по фильтрам
        /// </summary>
        /// <param name="filters">Условия фильтрации</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Количество уведомлений, соответствующих фильтрам</returns>
        public Task<int> GetTotalCountWithFiltersAsync(
            Expression<Func<Notification, bool>> filters,
            CancellationToken cancellationToken);
    }
}

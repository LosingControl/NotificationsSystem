using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NotificationService.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий для операций чтения с уведомлениями
    /// </summary>
    /// <remarks>
    /// Не изменяет состояние сущностей
    /// </remarks>
    public class NotificationQueryRepository : QueryRepository<Notification>, INotificationQueryRepository
    {
        public NotificationQueryRepository(AppDbContext bdContext) : base(bdContext)
        { }

        /// <summary>
        /// Получает пагинированный список уведомлений с фильтрацией
        /// </summary>
        /// <param name="filters">Выражение для фильтрации уведомлений</param>
        /// <param name="pageNumber">Номер страницы (начинается с 1)</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Асинхронный перечислитель уведомлений, соответствующих критериям фильтрации.
        /// Результаты отсортированы по дате создания (от новых к старым).
        /// </returns>
        public async IAsyncEnumerable<Notification> GetAllPaginatedFilteredAsync(
            Expression<Func<Notification, bool>> filters,
            int pageNumber, 
            int pageSize,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var query = _bdSet
                .AsNoTracking()
                .Where(filters)
                .OrderByDescending(n => n.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            await foreach (var item in query.AsAsyncEnumerable()
                .WithCancellation(cancellationToken))
            {
                yield return item;
            }
        }

        public async Task<int> GetTotalCountWithFiltersAsync(
            Expression<Func<Notification, bool>> filters,
            CancellationToken cancellationToken)
        {
            return await _bdSet
                .Where(filters)
                .CountAsync();
        }

        /// <summary>
        /// Получает список уведомлений по статусу
        /// </summary>
        /// <param name="status">Статус уведомлений</param>
        /// <returns>
        /// Список уведомлений с указанным статусом.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        public async Task<List<Notification>> GetByStatusAsync(NotificationStatus? status, CancellationToken cancellationToken)
        {
            return await _bdSet 
                .AsNoTracking()
                .Where(s => s.Status == status)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Получает список уведомлений пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>
        /// Список уведомлений, отсортированный по дате создания (новые сначала).
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        /// <exception cref="ArgumentException">Если передан пустой Guid</exception>
        public async Task<List<Notification>> GetByUserIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор пользователя не может быть пустым", nameof(id));
            }

            return await _bdSet
                .AsNoTracking()
                .Where (n => n.UserId == id)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}

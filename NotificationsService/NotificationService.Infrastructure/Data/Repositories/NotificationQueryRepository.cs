using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using NotificationService.Infrastructure.DTO_s;
using System.Linq.Expressions;
using System.Text.Json;

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
        public NotificationQueryRepository(
            AppDbContext bdContext, 
            IDistributedCache _distributedCache,
            DistributedCacheEntryOptions _cacheOptions) : base(bdContext, _distributedCache, _cacheOptions)
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
        /// Общее кол-во элементов.
        /// Результаты отсортированы по дате создания (от новых к старым).
        /// </returns>
        public async Task<PaginatedResultDTO<Notification>> GetAllPaginatedFilteredAsync(
            Expression<Func<Notification, bool>> filters,
            int pageNumber, 
            int pageSize,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"notifications:keys:{filters.ToString()}:{pageNumber}:{pageSize}";
            var cachedIds = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedIds != null)
            {
                return await GetAllFromCache(filters, cachedIds, cancellationToken);
            }

            var query = _bdSet
                .AsNoTracking()
                .Where(filters)
                .OrderByDescending(n => n.CreatedAt);

            var pageIds = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(pageIds),
                _cacheOptions,
                cancellationToken);

            var resultQuery = _bdSet
                .AsNoTracking()
                .Where(x => pageIds.Contains(x.Id))
                .OrderByDescending(n => n.CreatedAt);

            return new PaginatedResultDTO<Notification>(
                resultQuery.AsAsyncEnumerable(),
                await query.CountAsync(cancellationToken));
        }

        private async Task<PaginatedResultDTO<Notification>> GetAllFromCache(
            Expression<Func<Notification, bool>> filters, 
            string cachedIds, 
            CancellationToken cancellationToken)
        {
            var ids = JsonSerializer.Deserialize<List<Guid>>(cachedIds);

            var items = _bdSet
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .OrderByDescending(n => n.CreatedAt)
                .AsAsyncEnumerable();

            var totalCount = await GetTotalCountWithFiltersAsync(filters, cancellationToken);

            return new PaginatedResultDTO<Notification>(items, totalCount);
        }

        public async Task<int> GetTotalCountWithFiltersAsync(
            Expression<Func<Notification, bool>> filters,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"cauntSingl:{filters.ToString()}";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<int>(cachedData);
            }

            var count = await _bdSet
                .Where(filters)
                .CountAsync(cancellationToken);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(cachedData),
                _cacheOptions,
                cancellationToken); 

            return count;
        }

        /// <summary>
        /// Получает список уведомлений по статусу
        /// </summary>
        /// <param name="status">Статус уведомлений</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Список уведомлений с указанным статусом.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        public async Task<List<Notification>> GetByStatusAsync(NotificationStatus? status, CancellationToken cancellationToken)
        {
            var cacheKey = $"status:{status}";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedData != null) 
            {
                return JsonSerializer.Deserialize<List<Notification>>(cachedData);
            }

            var notifications = await _bdSet
                .AsNoTracking()
                .Where(s => s.Status == status)
                .ToListAsync(cancellationToken);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(notifications),
                _cacheOptions,
                cancellationToken);

            return notifications;
        }

        /// <summary>
        /// Получает список уведомлений пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
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

            var cacheKey = $"id:{id}";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
            
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<List<Notification>>(cachedData);
            }

            var notification = await _bdSet
                .AsNoTracking()
                .Where(n => n.UserId == id)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);

            await _distributedCache.SetStringAsync(
                cacheKey, 
                JsonSerializer.Serialize(notification), 
                _cacheOptions, 
                cancellationToken);

            return notification;
        }
    }
}

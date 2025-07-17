using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Abstractions.BaseRepositories
{
    /// <summary>
    /// Базовый абстрактный класс репозитория для запросов
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности репозитория</typeparam>
    public abstract class BaseQueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        protected readonly IDistributedCache _distributedCache;
        protected readonly DistributedCacheEntryOptions _cacheOptions;
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _bdSet;

        protected BaseQueryRepository(
            DbContext context, 
            IDistributedCache distributedCache,
            DistributedCacheEntryOptions cacheOptions)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Отсутствует контекст бд!", $"{nameof(context)},в {nameof(BaseQueryRepository<TEntity>)}");
            }

            _context = context;
            _bdSet = _context.Set<TEntity>();
            _cacheOptions = cacheOptions;
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// Получает список всех уведомлений в базе
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Возвращает список уведомлений.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var cacheKey = $"{typeof(TEntity).Name.ToLower()}:all";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<List<TEntity>>(cachedData);
            }

            var data = await _bdSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(data),
                _cacheOptions,
                cancellationToken);

            return data;
        }

        /// <summary>
        /// Получает уведомление по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// Найденное уведомление или null, если не найдено
        /// </returns>
        public virtual async Task<TEntity?> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var cacheKey = $"{typeof(TEntity).Name.ToLower()}: id:{id}";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<TEntity?>(cachedData);
            }

            var data = await _bdSet.FindAsync(id, cancellationToken);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(data),
                _cacheOptions,
                cancellationToken);

            return data;
        }
    }
}

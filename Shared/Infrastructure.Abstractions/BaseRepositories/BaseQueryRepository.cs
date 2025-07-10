using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions.BaseRepositories
{
    /// <summary>
    /// Базовый абстрактный класс репозитория для запросов
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности репозитория</typeparam>
    public abstract class BaseQueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _bdSet;

        protected BaseQueryRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Отсутствует контекст бд!", $"{nameof(context)},в {nameof(BaseQueryRepository<TEntity>)}");
            }

            _context = context;
            _bdSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Получает список всех уведомлений в базе
        /// </summary>
        /// <returns>
        /// Возвращает список уведомлений.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return _bdSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Получает уведомление по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <returns>
        /// Найденное уведомление или null, если не найдено
        /// </returns>
        public virtual async Task<TEntity?> GetByIdAsync(Guid? id)
        {
            return await _bdSet.FindAsync(id);
        }
    }
}

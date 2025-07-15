using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions.BaseRepositories
{
    /// <summary>
    /// Базовый абстрактный класс репозитория для команд
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности репозитория</typeparam>
    public abstract class BaseCommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _bdSet;

        protected BaseCommandRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Отсутствует контекст бд!", $"{nameof(context)},в {nameof(BaseCommandRepository<TEntity>)}");
            }

            _context = context;
            _bdSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Добавляет новое уведомление в базу данных
        /// </summary>
        /// <param name="entity">Добавляемое уведомление</param>
        /// <returns>
        /// true - если уведомление успешно добавлено,
        /// false - если произошла ошибка
        /// </returns>
        public virtual async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _bdSet.AddAsync(entity, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// <summary>
        /// Удаляет уведомление по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// <c>true</c> - если уведомление было успешно удалено,
        /// <c>false</c> - если уведомление с указанным ID не найдено
        /// </returns>
        public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _bdSet.FindAsync(id, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}

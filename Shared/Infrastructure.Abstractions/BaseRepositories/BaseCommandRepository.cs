using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            await _bdSet.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Удаляет уведомление по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <returns>
        /// <c>true</c> - если уведомление было успешно удалено,
        /// <c>false</c> - если уведомление с указанным ID не найдено
        /// </returns>
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _bdSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

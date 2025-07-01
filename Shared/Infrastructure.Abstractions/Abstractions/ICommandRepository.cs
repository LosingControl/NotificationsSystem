using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.Abstractions
{
    /// <summary>
    /// Базовый интерфейс репозитория для команд
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface ICommandRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавление новой сущности
        /// </summary>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        Task<bool> DeleteAsync(Guid id);
    }
}

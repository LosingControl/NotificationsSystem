using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.Abstractions
{
    /// <summary>
    /// Базовый интерфейс репозитория для запросов
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IQueryRepository<TEntity>  where TEntity : class
    {
        /// <summary>
        /// Получение сущности по ID
        /// </summary>
        Task<TEntity?> GetByIdAsync(Guid? id);

        /// <summary>
        /// Получение всех сущностей
        /// </summary>
        Task<List<TEntity>> GetAllAsync();
    }
}

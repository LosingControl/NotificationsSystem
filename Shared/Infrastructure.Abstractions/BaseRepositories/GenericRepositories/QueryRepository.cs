using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.BaseRepositories.GenericRepositories
{
    /// <summary>
    /// Реализация репозитория для запросов, R операций. Generic класс для DI
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public class QueryRepository<TEntity> : BaseQueryRepository<TEntity>, IQueryRepository<TEntity> 
        where TEntity : class
    {
        public QueryRepository(DbContext context) : base(context)
        { }
    }
}

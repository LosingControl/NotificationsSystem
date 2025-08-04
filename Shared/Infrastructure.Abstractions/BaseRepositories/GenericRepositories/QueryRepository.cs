using Infrastructure.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Abstractions.BaseRepositories.GenericRepositories
{
    /// <summary>
    /// Реализация репозитория для запросов, R операций. Generic класс для DI
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public class QueryRepository<TEntity> : BaseQueryRepository<TEntity>, IQueryRepository<TEntity> 
        where TEntity : class
    {
        public QueryRepository(
            DbContext context, 
            IDistributedCache _distributedCache, 
            DistributedCacheEntryOptions _cacheOptions) : base(context, _distributedCache, _cacheOptions)
        { }
    }
}

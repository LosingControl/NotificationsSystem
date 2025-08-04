using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NotificationService.Domain.Entities;
using System.ComponentModel;

namespace NotificationService.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий для операций записи с уведомлениями
    /// </summary>
    /// <remarks>
    /// <para><b>Ответственность:</b></para>
    /// <list type="number">
    ///   <item>Добавление новых уведомлений</item>
    ///   <item>Изменение существующих записей</item>
    ///   <item>Удаление уведомлений</item>
    /// </list>
    /// </remarks>
    public class NotificationCommandRepository : CommandRepository<Notification>, INotificationCommandRepository
    {
        public NotificationCommandRepository(
            AppDbContext bdContext, 
            IDistributedCache _distributedCache,
            DistributedCacheEntryOptions _cacheOptions) : base(bdContext, _distributedCache, _cacheOptions) 
        { }

        /// <summary>
        /// Удаляет уведомления по коллекции идентификаторов
        /// </summary>
        /// <param name="ids">Идентификаторы</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// <c>true</c> - если уведомление было успешно удалено,
        /// <c>false</c> - если уведомление с указанным ID не найдено
        /// </returns>
        public async Task<bool> ExecuteDeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            var deletedCount = await _bdSet
                .Where(x => ids.Contains(x.Id))
                .ExecuteDeleteAsync(cancellationToken);

            return deletedCount > 0;
        }
    }
}

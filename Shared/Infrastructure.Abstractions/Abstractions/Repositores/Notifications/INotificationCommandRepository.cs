
using System.Collections.Generic;

namespace Infrastructure.Abstractions.Abstractions.Repositores.Notifications
{
    /// <summary>
    /// Интерфейс для специфичных команд, свойственных только для уведомлений
    /// </summary>
    public interface INotificationCommandRepository
    {
        /// <summary>
        /// Удаляет уведомления по коллекции идентификаторов
        /// </summary>
        /// <param name="ids">Идентификаторы</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>
        /// <c>true</c> - если уведомление было успешно удалено,
        /// <c>false</c> - если уведомление с указанным ID не найдено
        /// </returns>
        public Task<bool> ExecuteDeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}

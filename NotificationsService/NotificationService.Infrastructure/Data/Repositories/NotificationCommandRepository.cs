using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Infrastructure.Abstractions.BaseRepositories.GenericRepositories;
using NotificationService.Domain.Entities;

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
        public NotificationCommandRepository(AppDbContext bdContext) : base(bdContext)
        { }
    }
}

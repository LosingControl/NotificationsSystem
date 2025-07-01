using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class NotificationCommandRepository : INotificationCommandRepository
    {
        private readonly AppDbContext _bdContext;

        public NotificationCommandRepository(AppDbContext bdContext)
        {
            _bdContext = bdContext;
        }

        /// <summary>
        /// Добавляет новое уведомление в базу данных
        /// </summary>
        /// <param name="entity">Добавляемое уведомление</param>
        /// <returns>
        /// true - если уведомление успешно добавлено,
        /// false - если произошла ошибка
        /// </returns>
        /// <exception cref="ArgumentNullException">Если передан null</exception>
        public async Task<bool> AddAsync(Notification entity)
        {
            if (entity == null) 
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _bdContext.Notifications.AddAsync(entity);

            return await _bdContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Удаляет уведомление по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <returns>
        /// <c>true</c> - если уведомление было успешно удалено,
        /// <c>false</c> - если уведомление с указанным ID не найдено
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если: Передан пустой Guid
        /// </exception>
        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("Идентификатор уведомления не может быть пустым", nameof(id));
            }

            try
            {
                int deletedCount = await _bdContext.Notifications
                    .Where(n => n.Id == id)
                    .ExecuteDeleteAsync();

                return deletedCount > 0;
            }
            catch (DbUpdateException ex)
            {
                // Необходимо сделать: Логирование ошибки, кастомные ошибки
                throw new NullReferenceException("Не удалось удалить уведомление либо оно не найдено", ex);
            }
        }
    }
}

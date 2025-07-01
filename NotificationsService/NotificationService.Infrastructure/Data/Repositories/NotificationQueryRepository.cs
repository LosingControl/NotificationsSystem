using Infrastructure.Abstractions.Abstractions;
using Infrastructure.Abstractions.Abstractions.Repositores.Notifications;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using NotificationService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий для операций чтения с уведомлениями
    /// </summary>
    /// <remarks>
    /// Не изменяет состояние сущностей
    /// </remarks>
    public class NotificationQueryRepository : INotificationQueryRepository
    {
        private readonly AppDbContext _bdContext;

        public NotificationQueryRepository(AppDbContext bdContext)
        {
            _bdContext = bdContext;
        }

        /// <summary>
        /// Получает список всех уведомлений в базе
        /// </summary>
        /// <returns>
        /// Список уведомлений, отсортированный по дате создания (новые сначала).
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        public async Task<List<Notification>> GetAllAsync()
        {
            return await _bdContext.Notifications
                .AsNoTracking()
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Получает уведомление по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <returns>
        /// Найденное уведомление или null, если не найдено
        /// </returns>
        /// <exception cref="ArgumentException">Если передан пустой Guid</exception>
        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Идентификатор не может быть пустым", nameof(id));

            return await _bdContext.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        /// <summary>
        /// Получает список уведомлений по статусу
        /// </summary>
        /// <param name="status">Статус уведомлений</param>
        /// <returns>
        /// Список уведомлений с указанным статусом.
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        public async Task<List<Notification>> GetByStatusAsync(NotificationStatus status)
        {
            return await _bdContext.Notifications 
                .AsNoTracking()
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        /// <summary>
        /// Получает список уведомлений пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>
        /// Список уведомлений, отсортированный по дате создания (новые сначала).
        /// Возвращает пустой список, если уведомлений не найдено.
        /// </returns>
        /// <exception cref="ArgumentException">Если передан пустой Guid</exception>
        public async Task<List<Notification>> GetByUserIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор пользователя не может быть пустым", nameof(id));
            }

            return await _bdContext.Notifications
                .AsNoTracking()
                .Where (n => n.UserId == id)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}

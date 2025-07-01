using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.Abstractions.Repositores.Notifications
{
    public interface INotificationQueryRepository : IQueryRepository<Notification>
    {
        public Task<List<Notification>> GetByStatusAsync(NotificationStatus status);
        public Task<List<Notification>> GetByUserIdAsync(Guid id);
    }
}

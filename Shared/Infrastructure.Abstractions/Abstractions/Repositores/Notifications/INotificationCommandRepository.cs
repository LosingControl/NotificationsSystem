using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.Abstractions.Repositores.Notifications
{
    public interface INotificationCommandRepository : ICommandRepository<Notification>
    {
    }
}

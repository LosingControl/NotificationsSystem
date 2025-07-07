using NotificationService.Domain.Entities;
using NotificationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Common.DTO_s
{
    /// <summary>
    /// Data Transfer Object (DTO) для фильтрации уведомлений.
    /// Позволяет задавать параметры фильтрации и пагинации при запросе списка уведомлений.
    /// </summary>
    public class NotificationFiltersDTO
    {
        /// <summary>
        /// Статус уведомления для фильтрации. Может быть null, если фильтрация по статусу не требуется.
        /// </summary>
        public NotificationStatus? Status { get; set; }

        /// <summary>
        /// Идентификатор уведомления для фильтрации. Может быть null, если фильтрация по ID не требуется.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя для фильтрации уведомлений. Может быть null, если фильтрация по пользователю не требуется.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Номер страницы для пагинации. По умолчанию равен 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Размер страницы (количество элементов на странице) для пагинации. По умолчанию равен 20.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Инициализирует новый экземпляр класса NotificationFiltersDTO с указанными параметрами фильтрации.
        /// </summary>
        /// <param name="status">Статус уведомления для фильтрации.</param>
        /// <param name="id">Идентификатор уведомления для фильтрации.</param>
        /// <param name="userId">Идентификатор пользователя для фильтрации.</param>
        /// <param name="pageNumber">Номер страницы для пагинации.</param>
        /// <param name="pageSize">Размер страницы для пагинации.</param>
        public NotificationFiltersDTO(NotificationStatus? status, Guid? id, Guid? userId, int pageNumber, int pageSize)
        {
            Status = status;
            Id = id;
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

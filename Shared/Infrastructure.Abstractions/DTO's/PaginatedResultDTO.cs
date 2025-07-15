using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.DTO_s
{
    /// <summary>
    /// Data Transfer Object (DTO) для представления пагинированного списка элементов.
    /// Содержит элементы, общее количество элементов.
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке</typeparam>
    public class PaginatedResultDTO<T>
    {
        /// <summary>
        /// Коллекция элементов асинхронного перечисления.
        /// </summary>
        public IAsyncEnumerable<T> Items { get; set; }

        /// <summary>
        /// Общее количество элементов.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса PaginatedResultDTO.
        /// </summary>
        /// <param name="items">Асинхронная коллекция элементов</param>
        /// <param name="totalCount">Общее количество элементов</param>
        public PaginatedResultDTO(IAsyncEnumerable<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}

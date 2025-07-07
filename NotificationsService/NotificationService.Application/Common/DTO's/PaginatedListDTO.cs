using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Common.Pagination
{
    /// <summary>
    /// Data Transfer Object (DTO) для представления пагинированного списка элементов.
    /// Содержит элементы текущей страницы, общее количество элементов и информацию о пагинации.
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке</typeparam>
    public class PaginatedListDTO<T>
    {
        /// <summary>
        /// Коллекция элементов текущей страницы в виде асинхронного перечисления.
        /// </summary>
        public IAsyncEnumerable<T> Items { get; set; }

        /// <summary>
        /// Общее количество элементов во всех страницах.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Номер текущей страницы. По умолчанию 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Количество элементов на странице. По умолчанию 20.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Общее количество страниц (вычисляемое свойство).
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Инициализирует новый экземпляр класса PaginatedListDTO.
        /// </summary>
        /// <param name="items">Асинхронная коллекция элементов текущей страницы</param>
        /// <param name="totalCount">Общее количество элементов</param>
        /// <param name="pageNumber">Номер текущей страницы</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        public PaginatedListDTO(IAsyncEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

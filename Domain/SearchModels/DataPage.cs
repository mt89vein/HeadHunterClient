using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.SearchModels
{
    /// <summary>
    /// Страница с объектами полученная в результате разбиения на страницы с помощью <see cref="PagedFilter"/>
    /// </summary>
    /// <typeparam name="TData">Тип данных</typeparam>
    public class DataPage<TData>
    {
        /// <summary>
        /// Общее количество объектов по запросу
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Объекты
        /// </summary>
        public IReadOnlyCollection<TData> Objects { get; set; } = new List<TData>();
    }
}
namespace Domain.Abstractions
{
    /// <summary>
    /// Фильтр пагинации
    /// </summary>
    public abstract class PagedFilter
    {
        /// <summary>
        /// Страница
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; } = 50;
    }
}
using Domain.SearchModels;

namespace Domain.Enums
{
    /// <summary>
    /// Где осуществлять поиск через поле <see cref="VacancyFilter.Text"/>
    /// </summary>
    public enum SearchField
    {
        /// <summary>
        /// В названии вакансии
        /// </summary>
        Name,

        /// <summary>
        /// В названии компании
        /// </summary>
        CompanyName,

        /// <summary>
        /// В описании вакансии
        /// </summary>
        Description,
    }
}
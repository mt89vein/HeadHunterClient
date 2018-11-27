namespace Domain.SearchModels
{
    /// <summary>
    /// Фильтр по зарплате
    /// </summary>
    public class SalaryFilter
    {
        /// <summary>
        /// Код валюты
        /// </summary>
        public string CurrencyCode { get; set; } = Constants.RussianCurrencyCode;

        /// <summary>
        /// Размер заработной платы
        /// </summary>
        public decimal? Salary { get; set; }

        /// <summary>
        /// Показывать вакансии только с указанием зарплаты
        /// </summary>
        public bool OnlyWithSalary { get; set; } = false;
    }
}
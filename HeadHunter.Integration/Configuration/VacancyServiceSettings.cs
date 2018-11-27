namespace HeadHunter.Integration.Configuration
{
    /// <summary>
    /// Настройки сервиса вакансий
    /// </summary>
    public class VacancyServiceSettings
    {
        /// <summary>
        /// Ссылка на получение вакансий
        /// </summary>
        public string EndpointUrl { get; set; }

        /// <summary>
        /// User-agent
        /// </summary>
        public string UserAgent { get; set; }
    }
}
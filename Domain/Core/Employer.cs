using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Core
{
    /// <summary>
    /// Работодатель https://hh.ru/employer/{ExternalId}
    /// </summary>
    public class Employer : ExternalEntity
    {
        public Employer(string name, string externalId, Area area, string siteUrl, string description, EmployerType? type,
            Logo logo) 
            : this(name, externalId, siteUrl, logo)
        {
            Area = area;
            Description = description;
            Type = type;
        }

        public Employer(string name, string externalId, string siteUrl, Logo logo)
        {
            Name = name;
            ExternalId = externalId;
            SiteUrl = siteUrl;
            Logo = logo;
        }

        private Employer()
        {
        }

        /// <summary>
        /// Наименование работодателя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Регион работодателя
        /// </summary>
        public Area Area { get; private set; }

        /// <summary>
        /// URL сайта работодателя
        /// </summary>
        public string SiteUrl { get; private set; }

        /// <summary>
        /// Описание работодателя
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Тип работодателя
        /// </summary>
        public EmployerType? Type { get; private set; }

        /// <summary>
        /// Логотип компании
        /// </summary>
        public Logo Logo { get; private set; }
    }
}
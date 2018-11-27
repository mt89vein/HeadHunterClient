using System;
using System.Collections.Generic;
using Domain.Abstractions;
using Domain.Enums;

namespace Domain.SearchModels
{
    /// <summary>
    /// Фильтр по вакансиям
    /// </summary>
    public class VacancyFilter : PagedFilter
    {
        public VacancyFilter()
        {
            SearchFields = new List<SearchField>();
            ScheduleTypes = new List<ScheduleType>();
            EmploymentTypes = new List<EmploymentType>();
            SpecializationExternalIds = new List<string>();
            SalaryFilter = new SalaryFilter();
        }

        /// <summary>
        /// Текстовый поиск
        /// </summary>
        public string Text { get; set; } = "c# developer";

        /// <summary>
        /// Где осуществлять поиск
        /// </summary>
        public IReadOnlyList<SearchField> SearchFields { get; set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public ExperienceType? Experience { get; set; }

        /// <summary>
        /// Тип занятости
        /// </summary>
        public IReadOnlyList<EmploymentType> EmploymentTypes { get; set; }

        /// <summary>
        /// График работы
        /// </summary>
        public IReadOnlyList<ScheduleType> ScheduleTypes { get; set; }

        ///// <summary>
        ///// Идентификатор региона
        ///// </summary>
        //public string AreaExternalId { get; set; }

        /// <summary>
        /// Идентификаторы специализаций
        /// </summary>
        public IReadOnlyList<string> SpecializationExternalIds { get; set; }

        /// <summary>
        /// Фильтр по зарплате
        /// </summary>
        public SalaryFilter SalaryFilter { get; set; }

        /// <summary>
        /// Идентификатор региона
        /// </summary>
        public string AreaExternalId { get; set; }

        /// <summary>
        /// Идентификатор метро
        /// </summary>
        public string MetroExternalId { get; set; }

        /// <summary>
        /// Идентификатор работодателя
        /// </summary>
        public string EmployerExternalId { get; set; }

        /// <summary>
        /// Количество дней, в пределах которых нужно найти вакансии.
        /// </summary>
        public int? Period { get; set; }

        /// <summary>
        /// Дата, которая ограничивает снизу диапазон дат публикации вакансий
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Дата, которая ограничивает сверху диапазон дат публикации вакансий.
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}
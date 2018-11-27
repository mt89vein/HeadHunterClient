using System;
using Domain.Core;

namespace Domain.EntityLinks
{
    /// <summary>
    /// Связь между вакансией и специализацией
    /// </summary>
    public class VacancySpecializationLink
    {
        public VacancySpecializationLink(long vacancyId, long specializationId)
        {
            VacancyId = vacancyId;
            SpecializationId = specializationId;
        }

        public VacancySpecializationLink(Vacancy vacancy, Specialization specialization)
        {
            Vacancy = vacancy;
            Specialization = specialization;
        }

        /// <summary>
        /// Идентификатор вакансии
        /// </summary>
        public long VacancyId { get; private set; }

        /// <summary>
        /// Вакансия
        /// </summary>
        public Vacancy Vacancy { get; private set; }

        /// <summary>
        /// Идентификатор специализации
        /// </summary>
        public long SpecializationId { get; private set; }

        /// <summary>
        /// Специализация
        /// </summary>
        public Specialization Specialization { get; private set; }
    }
}
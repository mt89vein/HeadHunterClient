using System;
using Domain.SearchModels;

namespace Domain.Enums
{
    /// <summary>
    /// Где осуществлять поиск через поле <see cref="VacancyFilter.Text" />
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
        Description
    }

    public static class HeadHunterDictionaryExtension
    {
        public static string ToHeadHunterFormat(this SearchField searchField)
        {
            switch (searchField)
            {
                case SearchField.Name:
                    return "name";
                case SearchField.CompanyName:
                    return "company_name";
                case SearchField.Description:
                    return "description";
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchField));
            }
        }

        public static string ToHeadHunterFormat(this ExperienceType? experience)
        {
            switch (experience)
            {
                case null:
                    return null;
                case ExperienceType.NoExperience:
                    return "noExperience";
                case ExperienceType.Between1And3:
                    return "between1And3";
                case ExperienceType.Between3And6:
                    return "between3And6";
                case ExperienceType.MoreThan6:
                    return "moreThan6";
                default:
                    throw new ArgumentOutOfRangeException(nameof(experience));
            }
        }

        public static string ToHeadHunterFormat(this ScheduleType scheduleType)
        {
            switch (scheduleType)
            {
                case ScheduleType.FullDay:
                    return "fullDay";
                case ScheduleType.Shift:
                    return "shift";
                case ScheduleType.Flexible:
                    return "flexible";
                case ScheduleType.Remote:
                    return "remote";
                case ScheduleType.FlyInFlyOut:
                    return "flyInFlyOut";
                default:
                    throw new ArgumentOutOfRangeException(nameof(scheduleType));
            }
        }
    }
}
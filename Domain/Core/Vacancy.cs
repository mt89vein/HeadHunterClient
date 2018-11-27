using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions;
using Domain.EntityLinks;
using Domain.Enums;

namespace Domain.Core
{
    /// <summary>
    /// Вакансия
    /// </summary>
    public class Vacancy : ExternalEntity
    {
        public Vacancy(string externalId, string name, VacancyType type, string description,
            IReadOnlyList<string> keySkills, Address address, ScheduleType? schedule, bool acceptHandicapped,
            bool acceptKids, ExperienceType? experience, EmploymentType? employment, Salary salary, bool archived,
            DateTime publishedAt, Department department, Contact contact, bool hasTest,
            bool? testRequired, Employer employer)
        {
            ExternalId = externalId;
            Name = name;
            Type = type;
            Description = description;
            KeySkills = keySkills;
            Address = address;
            Schedule = schedule;
            AcceptHandicapped = acceptHandicapped;
            AcceptKids = acceptKids;
            Experience = experience;
            Employment = employment;
            Salary = salary;
            Archived = archived;
            PublishedAt = publishedAt;
            Department = department;
            DepartmentId = department?.Id;
            Contact = contact;
            HasTest = hasTest;
            TestRequired = testRequired;
            Employer = employer;
        }

        private Vacancy()
        {
        }

        /// <summary>
        /// Название вакансии
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Тип вакансии
        /// </summary>
        public VacancyType Type { get; private set; }

        /// <summary>
        /// Описание вакансии
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Ключевые умения
        /// </summary>
        public IReadOnlyList<string> KeySkills { get; private set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// График работы
        /// </summary>
        public ScheduleType? Schedule { get; private set; }

        /// <summary>
        /// Признак доступности вакансии для соискателей с инвалидностью
        /// </summary>
        public bool AcceptHandicapped { get; private set; }

        /// <summary>
        /// Признак доступности вакансии для соискателей от 14 лет
        /// </summary>
        public bool AcceptKids { get; private set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public ExperienceType? Experience { get; private set; }

        /// <summary>
        /// Тип занятости
        /// </summary>
        public EmploymentType? Employment { get; private set; }

        /// <summary>
        /// Оклад
        /// </summary>
        public Salary Salary { get; private set; }

        /// <summary>
        /// Находится ли вакансия в архиве
        /// </summary>
        public bool Archived { get; private set; }

        /// <summary>
        /// Дата публикации вакансии
        /// </summary>
        public DateTime PublishedAt { get; private set; }

        /// <summary>
        /// Идентификатор департамента
        /// </summary>
        public long? DepartmentId { get; private set; }

        /// <summary>
        /// Департамент
        /// </summary>
        public Department Department { get; private set; }

        /// <summary>
        /// Контакты
        /// </summary>
        public Contact Contact { get; private set; }

        /// <summary>
        /// Имеется тестовое задание
        /// </summary>
        public bool HasTest { get; private set; }

        /// <summary>
        /// Обязательнось прохождения тестового задания
        /// </summary>
        public bool? TestRequired { get; private set; }

        /// <summary>
        /// Работодатель
        /// </summary>
        public Employer Employer { get; private set; }

        /// <summary>
        /// Идентификатор работодателя
        /// </summary>
        public long EmployerId { get; private set; }

        /// <summary>
        /// Ссылки на специализации
        /// </summary>
        private HashSet<VacancySpecializationLink> _vacancySpecializationLinks = new HashSet<VacancySpecializationLink>();

        public IEnumerable<VacancySpecializationLink> VacancySpecializationLinks => _vacancySpecializationLinks.ToList();

        public void AddSpecialization(Specialization specialization)
        {
            _vacancySpecializationLinks.Add(new VacancySpecializationLink(this, specialization));
        }
    }
}
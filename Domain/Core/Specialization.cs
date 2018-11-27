using System;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Специализация
    /// </summary>
    public class Specialization : ExternalEntity
    {
        public Specialization(string externalId, string name, bool? laboring, ProfessionalArea professionalArea) : this(externalId, name, laboring)
        {
            ProfessionalArea = professionalArea;
        }

        public Specialization(string externalId, string name, bool? laboring) 
        {
            ExternalId = externalId;
            Name = name;
            Laboring = laboring;
        }

        private Specialization()
        {
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Признак рабочей специальности
        /// </summary>
        public bool? Laboring { get; private set; }

        /// <summary>
        /// Профессиональная область, в которую входит специализация
        /// </summary>
        public ProfessionalArea ProfessionalArea { get; private set; }

        /// <summary>
        /// Идентификатор профессиональной области, в которую входит специализация
        /// </summary>
        public long ProfessionalAreaId { get; private set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Профессиональная область
    /// </summary>
    public class ProfessionalArea : ExternalEntity
    {
        public ProfessionalArea(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }

        private ProfessionalArea()
        {
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Специализации
        /// </summary>
        public IReadOnlyCollection<Specialization> Specializations => _specializations.ToList();

        private readonly List<Specialization> _specializations = new List<Specialization>();

        public void AddSpecialization(Specialization specialization)
        {
            _specializations.Add(specialization);
        }
    }
}
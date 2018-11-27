using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Департамент, от имени которого размещается вакансия
    /// </summary>
    public class Department : ExternalEntity
    {
        private Department()
        {
        }

        public Department(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }

        /// <summary>
        /// Наименование департамента
        /// </summary>
        public string Name { get; private set; }
    }
}
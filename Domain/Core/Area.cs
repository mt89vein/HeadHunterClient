using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Регион размещения вакансии  https://api.hh.ru/areas/{externalId}
    /// </summary>
    public class Area : Entity
    {
        public Area(long externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }

        private Area()
        {
        }

        /// <summary>
        /// Идентификатор региона в hh.ru
        /// </summary>
        public long ExternalId { get; private set; }

        /// <summary>
        /// Название региона
        /// </summary>
        public string Name { get; private set; }
    }
}
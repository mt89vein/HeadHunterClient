using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    /// <summary>
    /// Модель специализации
    /// </summary>
    public class SpecializationJsonModel : IHeadHunterJsonModel<Specialization>
    {
        /// <summary>
        /// Идентификатор специализации
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Является ли рабочей профессией
        /// </summary>
        [JsonProperty("laboring")]
        public bool Laboring { get; set; }

        public Specialization GetModel()
        {
            return new Specialization(Id, Name, Laboring);
        }
    }
}

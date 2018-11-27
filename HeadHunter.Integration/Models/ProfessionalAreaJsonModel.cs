using System.Collections.Generic;
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    /// <summary>
    /// Модель проф. области
    /// </summary>
    public class ProfessionalAreaJsonModel : IHeadHunterJsonModel<ProfessionalArea>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Специализации проф. области
        /// </summary>
        [JsonProperty("specializations")]
        public List<SpecializationJsonModel> Specializations { get; set; }

        public ProfessionalArea GetModel()
        {
            var professionalArea = new ProfessionalArea(Id, Name);
            Specializations.ForEach(s => professionalArea.AddSpecialization(s.GetModel()));
            return professionalArea;
        }
    }
}
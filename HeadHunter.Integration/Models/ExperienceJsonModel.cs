using Domain.Enums;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class ExperienceJsonModel : HeadHunterEnumJsonModel<ExperienceType>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        protected override string GetEnumProperty()
        {
            return Id;
        }
    }
}
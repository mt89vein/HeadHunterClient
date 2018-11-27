using Newtonsoft.Json;
using Domain.Enums;
using HeadHunter.Integration.Abstractions;

namespace HeadHunter.Integration.Models
{
    public class VacancyTypeJsonModel : HeadHunterEnumJsonModel<VacancyType>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        protected override string GetEnumProperty()
        {
            return Id;
        }
    }
}

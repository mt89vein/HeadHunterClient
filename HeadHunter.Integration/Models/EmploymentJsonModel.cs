using Domain.Enums;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class EmploymentJsonModel : HeadHunterEnumJsonModel<EmploymentType>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        protected override string GetEnumProperty()
        {
            return Id;
        }
    }
}
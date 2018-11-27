using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class KeySkillJsonModel : IHeadHunterJsonModel<string>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public string GetModel()
        {
            return Name;
        }
    }
}
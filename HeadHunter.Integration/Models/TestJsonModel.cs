using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class TestJsonModel : IHeadHunterJsonModel<bool>
    {
        [JsonProperty("required")]
        public bool TestRequired { get; set; }

        public bool GetModel()
        {
            return TestRequired;
        }
    }
}
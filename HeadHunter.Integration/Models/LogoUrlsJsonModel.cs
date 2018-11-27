using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class LogoUrlsJsonModel : IHeadHunterJsonModel<Logo>
    {
        [JsonProperty("90")]
        public string The90 { get; set; }

        [JsonProperty("240")]
        public string The240 { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        public Logo GetModel()
        {
            return new Logo(The90, The240, Original);
        }
    }
}
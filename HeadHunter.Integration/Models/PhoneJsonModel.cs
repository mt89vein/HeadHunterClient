using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class PhoneJsonModel : IHeadHunterJsonModel<PhoneNumber>
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        public PhoneNumber GetModel()
        {
            return new PhoneNumber(Country, City, Number, Comment);
        }
    }
}
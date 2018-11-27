using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class AddressJsonModel : IHeadHunterJsonModel<Address>
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("lat")]
        public decimal? Lat { get; set; }

        [JsonProperty("lng")]
        public decimal? Lng { get; set; }

        public Address GetModel()
        {
            return new Address(Street,City,Building, Description, Lng, Lat);
        }
    }
}
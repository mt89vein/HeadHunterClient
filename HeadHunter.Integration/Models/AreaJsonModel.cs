using System;
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class AreaJsonModel : IHeadHunterJsonModel<Area>
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public Area GetModel()
        {
            return new Area(Id, Name);
        }
    }
}
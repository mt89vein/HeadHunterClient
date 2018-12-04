using System;
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class EmployerJsonModel : IHeadHunterJsonModel<Employer>
    {
        [JsonProperty("logo_urls")]
        public LogoUrlsJsonModel LogoUrlsJsonModel { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("alternate_url")]
        public Uri AlternateUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("trusted")]
        public bool Trusted { get; set; }

        [JsonProperty("blacklisted")]
        public bool Blacklisted { get; set; }

        [JsonProperty("area")]
        public AreaJsonModel Area { get; set; }

        public Employer GetModel()
        {
            return new Employer(Name, Id, Url?.ToString(), LogoUrlsJsonModel?.GetModel());
        }
    }
}
using System;
using Domain.Enums;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class DriverLicenseTypeJsonModel : IHeadHunterJsonModel<DriverLicenseType>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public DriverLicenseType GetModel()
        {
            return (DriverLicenseType) Enum.Parse(typeof(DriverLicenseType), Id, true);
        }
    }
}
using System;
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class DepartmentJsonModel : IHeadHunterJsonModel<Department>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public Department GetModel()
        {
            return String.IsNullOrWhiteSpace(Id) ? null : new Department(Id, Name);
        }
    }
}
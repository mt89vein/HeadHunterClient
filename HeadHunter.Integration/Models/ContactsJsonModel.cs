using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class ContactsJsonModel : IHeadHunterJsonModel<Contact>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phones")]
        public List<PhoneJsonModel> Phones { get; set; }

        public Contact GetModel()
        {
            return new Contact(Name, Email, Phones.Select(p => p.GetModel()).ToList());
        }
    }
}
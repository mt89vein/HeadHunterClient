using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class SalaryJsonModel : IHeadHunterJsonModel<Salary>
    {
        [JsonProperty("to")]
        public decimal? To { get; set; }

        [JsonProperty("from")]
        public decimal? From { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("gross")]
        public bool? Gross { get; set; }

        public Salary GetModel()
        {
            return new Salary(From, To, Currency, Gross);
        }
    }
}
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    /// <summary>
    /// Модель валюты
    /// </summary>
    public class CurrencyJsonModel : IHeadHunterJsonModel<Currency>
    {
        /// <summary>
        /// Код валюты
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Аббревиатура
        /// </summary>
        [JsonProperty("abbr")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Является валютой по умолчанию
        /// </summary>
        [JsonProperty("default")]
        public bool Default { get; set; }

        /// <summary>
        /// Валюта актуальна и используется
        /// </summary>
        [JsonProperty("in_use")]
        public bool Actual { get; set; }

        public Currency GetModel()
        {
            return new Currency(Code, Abbreviation, Name, Rate, Default, Actual);
        }
    }
}
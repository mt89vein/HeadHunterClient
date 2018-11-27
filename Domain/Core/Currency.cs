using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency : ExternalEntity
    {
        public Currency(string code, string abbreviation, string name, decimal rate, bool @default, bool actual)
        {
            Code = code;
            Abbreviation = abbreviation;
            Name = name;
            Rate = rate;
            Default = @default;
            Actual = actual;
        }

        private Currency()
        {
        }

        public new string ExternalId => Code;
 
        /// <summary>
        /// Код валюты
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Аббревиатура
        /// </summary>
        public string Abbreviation { get; private set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Курс
        /// </summary>
        public decimal Rate { get; private set; }

        /// <summary>
        /// Является валютой по умолчанию
        /// </summary>
        public bool Default { get; private set; }

        /// <summary>
        /// Валюта актуальна и используется
        /// </summary>
        public bool Actual { get; private set; }
    }
}

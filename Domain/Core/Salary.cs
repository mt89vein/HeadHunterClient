using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Заработная плата
    /// </summary>
    public class Salary : ValueObject
    {
        private Salary()
        {
        }

        public Salary(decimal? from, decimal? to, string currency, bool? gross)
        {
            From = from;
            To = to;
            Currency = currency;
            Gross = gross;
        }

        /// <summary>
        /// От
        /// </summary>
        public decimal? From { get; private set; }

        /// <summary>
        /// До
        /// </summary>
        public decimal? To { get; private set; }

        /// <summary>
        /// Код валюты
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Признак указания заработной платы до вычета налогов
        /// </summary>
        public bool? Gross { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return From;
            yield return To;
            yield return Currency;
            yield return Gross;
        }

        public static Salary Empty()
        {
            return new Salary(null, null, null, null);
        }
    }
}
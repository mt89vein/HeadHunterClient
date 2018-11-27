using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public class PhoneNumber : ValueObject
    {
        private PhoneNumber()
        {
        }

        public PhoneNumber(string country, string city, string number, string comment)
        {
            Country = country;
            City = city;
            Number = number;
            Comment = comment;
        }

        /// <summary>
        /// Код страны
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Код города
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; private set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Country;
            yield return City;
            yield return Number;
            yield return Comment;
        }
    }
}
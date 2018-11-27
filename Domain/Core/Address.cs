using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address : ValueObject
    {
        private Address()
        {
        }

        public Address(string street, string city, string building, string description, decimal? longitude,
            decimal? latitude)
        {
            Street = street;
            City = city;
            Building = building;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Здание
        /// </summary>
        public string Building { get; private set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Широта
        /// </summary>
        public decimal? Latitude { get; private set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public decimal? Longitude { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return Building;
            yield return Description;
            yield return Latitude;
            yield return Longitude;
        }

        public static Address Empty()
        {
            return new Address(null, null, null, null, (decimal?)null, (decimal?)null);
        }
    }
}
using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Контакты
    /// </summary>
    public class Contact : ValueObject
    {
        private Contact()
        {
        }

        public Contact(string name, string email, IReadOnlyList<PhoneNumber> phoneNumbers)
        {
            Name = name;
            Email = email;
            PhoneNumbers = phoneNumbers;
        }

        /// <summary>
        /// Имя контактного лица
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Email контактного лица
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Номера телефонов
        /// </summary>
        public IReadOnlyList<PhoneNumber> PhoneNumbers { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Email;
            yield return PhoneNumbers;
        }

        public static Contact Empty()
        {
            return new Contact(null, null, new List<PhoneNumber>());
        }
    }
}
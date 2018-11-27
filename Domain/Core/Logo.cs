using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Core
{
    /// <summary>
    /// Логотип
    /// </summary>
    public class Logo : ValueObject
    {
        private Logo()
        {
        }

        public Logo(string smallSize, string mediumSize, string originalSize)
        {
            SmallSize = smallSize;
            MediumSize = mediumSize;
            OriginalSize = originalSize;
        }

        /// <summary>
        /// Ссылка на изображение (90x90)
        /// </summary>
        public string SmallSize { get; private set; }

        /// <summary>
        /// Ссылка на изображение (240x240)
        /// </summary>
        public string MediumSize { get; private set; }

        /// <summary>
        /// Ссылка на оригинальное изображение
        /// </summary>
        public string OriginalSize { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return SmallSize;
            yield return MediumSize;
            yield return OriginalSize;
        }

        public static Logo Empty()
        {
            return new Logo(null, null, null);
        }
    }
}
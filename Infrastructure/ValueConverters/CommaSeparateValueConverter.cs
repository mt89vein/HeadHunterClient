using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.ValueConverters
{
    internal class CommaSeparateValueConverter<T> : ValueConverter<IReadOnlyList<T>, string>
    {
        public CommaSeparateValueConverter(ConverterMappingHints mappingHints = null) 
            : base(input => string.Join(";", input), output => output.Split(';').Cast<T>().ToList(), mappingHints)
        {
        }
    }
}
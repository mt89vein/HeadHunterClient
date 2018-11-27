using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.ValueConverters
{
    internal class JsonValueConverter<T> : ValueConverter<IReadOnlyList<T>, string>
    {
        public JsonValueConverter(ConverterMappingHints mappingHints = null) 
            : base(input => JsonConvert.SerializeObject(input), output => JsonConvert.DeserializeObject<IReadOnlyList<T>>(output), mappingHints)
        {
        }
    }
}
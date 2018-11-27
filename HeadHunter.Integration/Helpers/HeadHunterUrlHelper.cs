using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HeadHunter.Integration.Helpers
{
    /// <summary>
    /// Помощник по построению URL
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Конвертировать объект в QueryString
        /// </summary>
        /// <param name="obj">Конвертируемый объект</param>
        /// <returns>QueryString</returns>
        public static Task<string> ToQueryStringAsync(object obj)
        {
            var keyValueFilter = obj.ToKeyValue();
            return new FormUrlEncodedContent(keyValueFilter).ReadAsStringAsync();
        }

        /// <summary>
        /// Конвертировать объект в словарь ключ-значение
        /// </summary>
        /// <param name="metaToken"></param>
        /// <returns></returns>
        private static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                            .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                jValue?.ToString("o", CultureInfo.InvariantCulture) :
                jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
    }
}

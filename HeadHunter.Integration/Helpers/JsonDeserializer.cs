using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions;
using Domain.SearchModels;
using HeadHunter.Integration.Abstractions;
using HeadHunter.Integration.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace HeadHunter.Integration.Helpers
{
    /// <summary>
    /// Класс обёртка по десериализации Json в сущности
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    /// <typeparam name="THeadHunterModel">Json-модель headhunter </typeparam>
    public class JsonDeserializer<TEntity, THeadHunterModel>
        where THeadHunterModel : IHeadHunterJsonModel<TEntity>
        where TEntity : Entity
    {
        private readonly ILogger<JsonDeserializer<TEntity, THeadHunterModel>> _logger;

        public JsonDeserializer(ILogger<JsonDeserializer<TEntity, THeadHunterModel>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool TryDeserializeContent(string content, out IEnumerable<TEntity> entities, string propertyToParse = null)
        {
            entities = Enumerable.Empty<TEntity>();
            try
            {
                if (propertyToParse != null)
                {
                    var jsonObject = JObject.Parse(content);
                    if (jsonObject.TryGetValue(propertyToParse, out var jsonArray))
                    {
                        entities = jsonArray.ToObject<IEnumerable<THeadHunterModel>>().Select(h => h.GetModel());
                    }
                }
                else
                {
                    var jsonArray = JArray.Parse(content);
                    entities = jsonArray.ToObject<IEnumerable<THeadHunterModel>>().Select(h => h.GetModel());
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при десериализации JSON");

                return false;
            }
        }
    }    
    
    /// <summary>
    /// Класс обёртка по десериализации Json страниц в сущности
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    /// <typeparam name="THeadHunterModel">Json-модель headhunter </typeparam>
    public class JsonDataPageDeserializer<TEntity, THeadHunterModel>
        where THeadHunterModel : IHeadHunterJsonModel<DataPage<TEntity>>
        where TEntity : Entity
    {
        private readonly ILogger<JsonDataPageDeserializer<TEntity, THeadHunterModel>> _logger;

        public JsonDataPageDeserializer(ILogger<JsonDataPageDeserializer<TEntity, THeadHunterModel>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool TryDeserializeContent(string content, out DataPage<TEntity> entities)
        {
            entities = new DataPage<TEntity>();
            try
            {
                var jObject = JObject.Parse(content);
                entities = jObject.ToObject<THeadHunterModel>().GetModel();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при десериализации JSON");

                return false;
            }
        }
    }
}

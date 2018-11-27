using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Services;
using HeadHunter.Integration.Abstractions;
using HeadHunter.Integration.Helpers;
using Microsoft.Extensions.Logging;

namespace HeadHunter.Integration.Services
{
    public abstract class SynchronizableService<TExternalEntity, THeadHunterModel> : BaseService<TExternalEntity>,
        ISynchronizableService<TExternalEntity> 
        where TExternalEntity : ExternalEntity
        where THeadHunterModel : IHeadHunterJsonModel<TExternalEntity>
    {
        private readonly ILogger<SynchronizableService<TExternalEntity, THeadHunterModel>> _logger;
        private readonly SynchronizerSettings _synchronizerSettings;
        private readonly JsonDeserializer<TExternalEntity, THeadHunterModel> _jsonDeserializer;
        private static HttpClient _httpClient = new HttpClient();

        protected SynchronizableService(
            SynchronizerSettings synchronizerSettings,
            ILogger<SynchronizableService<TExternalEntity, THeadHunterModel>> logger,
            JsonDeserializer<TExternalEntity, THeadHunterModel> jsonDeserializer)
        {
            _synchronizerSettings =
                synchronizerSettings ?? throw new ArgumentNullException(nameof(synchronizerSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jsonDeserializer = jsonDeserializer;
        }

        public async Task Synchronize(CancellationToken cancellationToken)
        {
            var actualEntities = await GetActualEntitiesAsync(cancellationToken);
            await UpdateEntitiesAsync(actualEntities, cancellationToken);
        }

        public TimeSpan Periodicity => TimeSpan.FromMinutes(_synchronizerSettings.Periodicity);

        protected abstract Task UpdateEntitiesAsync(IEnumerable<TExternalEntity> actualEntities, CancellationToken cancellationToken);

        private async Task<IEnumerable<TExternalEntity>> GetActualEntitiesAsync(CancellationToken cancellationToken)
        {
            var result = Enumerable.Empty<TExternalEntity>();
            try
            {
                var httpResponse = await _httpClient.GetAsync(_synchronizerSettings.EndpointUrl, cancellationToken);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        $"{_synchronizerSettings.EndpointUrl} недоступен {httpResponse.StatusCode}");

                    return Enumerable.Empty<TExternalEntity>();
                }

                var sResult = await httpResponse.Content.ReadAsStringAsync();

                if (_jsonDeserializer.TryDeserializeContent(sResult, out var entities, _synchronizerSettings.PropertyName))
                {
                    return entities;
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при обработке курсов валют");
            }

            return result;
        }
    }
}
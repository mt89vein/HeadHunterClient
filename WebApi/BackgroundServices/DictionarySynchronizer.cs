using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi.BackgroundServices
{
    /// <summary>
    /// Фоновая задача для синхронизации справочников
    /// </summary>
    public class DictionarySynchronizer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DictionarySynchronizer> _logger;
        private readonly Dictionary<Timer, IServiceScope> _timerScopes;

        public DictionarySynchronizer(IServiceProvider serviceProvider, ILogger<DictionarySynchronizer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timerScopes = new Dictionary<Timer, IServiceScope>();
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            {
                var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<ICurrencyService>();
                var timer = new Timer(async _ => { await service.Synchronize(cancellationToken); }, null, TimeSpan.Zero, service.Periodicity);
                _timerScopes.Add(timer, scope);
            }
            {
                var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IProfessionalAreaService>();
                var timer = new Timer(async _ => { await service.Synchronize(cancellationToken); }, null, TimeSpan.Zero, service.Periodicity);
                _timerScopes.Add(timer, scope);
            }

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            foreach (var timerScope in _timerScopes)
            {
                timerScope.Value?.Dispose();
                timerScope.Key?.Dispose();
            }
            base.Dispose();
        }
    }
}

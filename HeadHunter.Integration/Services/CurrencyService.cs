using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Services;
using HeadHunter.Integration.Configuration;
using HeadHunter.Integration.Helpers;
using HeadHunter.Integration.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HeadHunter.Integration.Services
{
    public class CurrencyService : SynchronizableService<Currency, CurrencyJsonModel>, ICurrencyService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(
            IOptions<CurrencySynchronizerSettings> currencySynchronizerSettings,
            ApplicationContext context,
            JsonDeserializer<Currency, CurrencyJsonModel> jsonDeserializer,
            ILogger<CurrencyService> logger) 
            : base(currencySynchronizerSettings.Value, logger, jsonDeserializer)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task UpdateEntitiesAsync(IEnumerable<Currency> actualCurrencies, CancellationToken cancellationToken)
        {
            if (!actualCurrencies.Any())
            {
                return;
            }

            try
            {
                var currentCurrenciesDictionary =
                    await _context.Currencies.AsNoTracking().ToDictionaryAsync(c => c.Code, cancellationToken);

                foreach (var currency in actualCurrencies)
                {
                    if (!currentCurrenciesDictionary.ContainsKey(currency.Code))
                    {
                        _context.Currencies.Add(currency);
                    }
                    else
                    {
                        _context.Entry(currency).Property(p => p.Id).CurrentValue =
                            currentCurrenciesDictionary[currency.Code].Id;
                        _context.Currencies.Update(currency);
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при сохранении данных справочника валют");
            }
        }

        public override void Save(Currency currency)
        {
            var persistedCurrency = _context.Currencies.AsNoTracking().FirstOrDefault(d => d.Code.Equals(currency.Code));

            if (persistedCurrency != null)
            {
                _context.Entry(currency).Property(v => v.Id).CurrentValue = persistedCurrency.Id;
                _context.Currencies.Update(currency);
            }
            else
            {
                _context.Currencies.Add(currency);
            }

            _context.SaveChanges();
        }

        public async Task<IReadOnlyList<Currency>> GetByQueryAsync(string query)
        {
            return await _context.Currencies
                .Where(c => c.Abbreviation.Contains(query) || c.Name.Contains(query))
                .Take(100)
                .ToListAsync();
        }
    }
}
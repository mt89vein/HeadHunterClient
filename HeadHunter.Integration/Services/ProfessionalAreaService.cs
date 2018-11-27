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
    /// <summary>
    /// Сервис по работе с прроф областями и их специализациями
    /// </summary>
    public class ProfessionalAreaService : SynchronizableService<ProfessionalArea, ProfessionalAreaJsonModel>,
        IProfessionalAreaService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ProfessionalAreaService> _logger;

        public ProfessionalAreaService(
            IOptions<ProfessionalAreaSynchronizerSettings> professionalAreaSynchronizerSettings,
            ApplicationContext context,
            JsonDeserializer<ProfessionalArea, ProfessionalAreaJsonModel> jsonDeserializer,
            ILogger<ProfessionalAreaService> logger) 
            : base(professionalAreaSynchronizerSettings.Value, logger, jsonDeserializer)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Сохранить проф. области вместе со специализациями
        /// </summary>
        /// <param name="actualProfessionalAreas">Список проф. областтей</param>
        /// <returns>awaitable task</returns>
        protected override async Task UpdateEntitiesAsync(IEnumerable<ProfessionalArea> actualProfessionalAreas, CancellationToken cancellationToken)
        {
            if (!actualProfessionalAreas.Any())
            {
                return;
            }

            try
            {
                var currentProfessionalAreasDictionary =
                    await _context.ProfessionalAreas.AsNoTracking().ToDictionaryAsync(c => c.ExternalId, cancellationToken);

                foreach (var professionalArea in actualProfessionalAreas)
                {
                    if (!currentProfessionalAreasDictionary.ContainsKey(professionalArea.ExternalId))
                    {
                        _context.ProfessionalAreas.Add(professionalArea);
                    }
                    else
                    {
                        var currentProfessionalId = currentProfessionalAreasDictionary[professionalArea.ExternalId].Id;
                        _context.Entry(professionalArea).Property(p => p.Id).CurrentValue = currentProfessionalId;
                        _context.ProfessionalAreas.Update(professionalArea);

                        SaveSpecializations(professionalArea.Specializations);
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при сохранении данных справочника проф. областей");
            }
        }

        /// <summary>
        /// Сохранить специализации, если их нет - добавить, если есть обновить
        /// </summary>
        /// <param name="specializations">Список специализаций</param>
        /// <returns>awaitable task</returns>
        public void SaveSpecializations(IReadOnlyCollection<Specialization> specializations)
        {
            if (!specializations.Any())
            {
                return;
            }

            var extIds = specializations.Select(s => s.ExternalId).ToList();

            var persistedSpecializationsDictionary =  _context.Specializations.AsNoTracking()
                .Where(sp => extIds.Contains(sp.ExternalId)).ToDictionary(s => s.ExternalId);

            foreach (var specialization in specializations)
            {
                var entry = _context.Entry(specialization);
                if (!persistedSpecializationsDictionary.ContainsKey(specialization.ExternalId))
                {
                    entry.State = EntityState.Added;
                }
                else
                {
                    var currentSpecialization = persistedSpecializationsDictionary[specialization.ExternalId];
                    entry.Property(p => p.Id).CurrentValue = currentSpecialization.Id;
                    entry.Property(p => p.ProfessionalAreaId).CurrentValue = currentSpecialization.ProfessionalAreaId;
                    entry.State = EntityState.Modified;
                }
            }
        }

        public override void Save(ProfessionalArea professionalArea)
        {
            var persistedProfessionalArea = _context.ProfessionalAreas.AsNoTracking().FirstOrDefault(d => d.ExternalId.Equals(professionalArea.ExternalId));

            if (persistedProfessionalArea != null)
            {
                _context.Entry(professionalArea).Property(v => v.Id).CurrentValue = persistedProfessionalArea.Id;
                _context.ProfessionalAreas.Update(professionalArea);
            }
            else
            {
                _context.ProfessionalAreas.Add(professionalArea);
            }

            _context.SaveChanges();
        }
    }
}
using System;
using System.Linq;
using Domain.Core;
using Domain.Services;
using HeadHunter.Integration.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HeadHunter.Integration.Services
{
    public class EmployerService : BaseService<Employer>, IEmployerService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EmployerService> _logger;

        public EmployerService(ApplicationContext context, ILogger<EmployerService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Сохранить рработодателя, если его еще нет - добавить, если есть обновить
        /// </summary>
        /// <param name="employer">Работодатель</param>
        public override void Save(Employer employer)
        {
            var persistedEmployer = _context.Employers.AsNoTracking().FirstOrDefault(e => e.ExternalId.Equals(employer.ExternalId));

            if (persistedEmployer != null)
            {
                _context.Entry(employer).Property(v => v.Id).CurrentValue = persistedEmployer.Id;
                _context.Employers.Update(employer);
            }
            else
            {
                _context.Employers.Add(employer);
            }

            _context.SaveChanges();
        }
    }
}
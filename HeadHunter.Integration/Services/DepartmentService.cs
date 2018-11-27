using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Services;
using HeadHunter.Integration.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HeadHunter.Integration.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(ApplicationContext context, ILogger<DepartmentService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void Save(Department department)
        {
            var persistedDepartment = _context.Departments.AsNoTracking().FirstOrDefault(d => d.ExternalId.Equals(department.ExternalId));
            if (persistedDepartment != null)
            {
                _context.Entry(department).Property(v => v.Id).CurrentValue = persistedDepartment.Id;
                _context.Departments.Update(department);
            }
            else
            {
                _context.Departments.Add(department);
            }

            _context.SaveChanges();
        }
    }
}
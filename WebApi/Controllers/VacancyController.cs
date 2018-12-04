using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core;
using Domain.SearchModels;
using Domain.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IEmployerService _employerService;
        private readonly IDepartmentService _departmentService;

        public VacancyController(
            IVacancyService vacancyService, 
            IEmployerService employerService, 
            IDepartmentService departmentService
            )
        {
            _vacancyService = vacancyService ?? throw new ArgumentNullException(nameof(vacancyService));
            _employerService = employerService ?? throw new ArgumentNullException(nameof(employerService));
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        [HttpGet]
        public async Task<ActionResult<DataPage<Vacancy>>> Index([FromQuery] VacancyFilter filter)
        {
            var result = await _vacancyService.GetFilteredAsync(filter);

            if (result.Objects.Any(w => w.Id == 0))
            {
                var vacancies = result.Objects.GroupBy(v => v.ExternalId).Select(vg => vg.FirstOrDefault()).ToList();

                var departments = vacancies.Where(v => v.Department != null).Select(v => v.Department).GroupBy(d => d.ExternalId).Select(dg => dg.FirstOrDefault()).ToList();
                foreach (var department in departments)
                {
                    _departmentService.Save(department);
                }

                var employers = vacancies.Where(v => v.Employer != null).Select(v => v.Employer).GroupBy(e => e.ExternalId).Select(eg => eg.FirstOrDefault()).ToList();
                foreach (var employer in employers)
                {
                    _employerService.Save(employer);
                }

                foreach (var vacancy in vacancies)
                {
                    _vacancyService.Save(vacancy);
                }
            }

            return result;
        }
    }
}
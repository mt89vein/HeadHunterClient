using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Core;
using Domain.EntityLinks;
using Domain.Enums;
using Domain.SearchModels;
using Domain.Services;
using HeadHunter.Integration.Abstractions;
using HeadHunter.Integration.Configuration;
using HeadHunter.Integration.Helpers;
using HeadHunter.Integration.Models;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HeadHunter.Integration.Services
{
    public class VacancyService : BaseService<Vacancy>, IVacancyService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<VacancyService> _logger;
        private readonly VacancyServiceSettings _settings;
        private readonly JsonDataPageDeserializer<Vacancy, HeadHunterDataPage<Vacancy, VacancyJsonModel>> _jsonDeserializer;
        private readonly HttpClient _httpClient;

        public VacancyService(
            ApplicationContext context, 
            ILogger<VacancyService> logger,
            IOptions<VacancyServiceSettings> settings,
            JsonDataPageDeserializer<Vacancy, HeadHunterDataPage<Vacancy, VacancyJsonModel>> jsonDeserializer)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _jsonDeserializer = jsonDeserializer ?? throw new ArgumentNullException(nameof(jsonDeserializer));
            _httpClient = new HttpClient {
                BaseAddress = new Uri(_settings.EndpointUrl),
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", _settings.UserAgent);
        }

        public override void Save(Vacancy vacancy)
        {
            var persistedVacancy = _context.Vacancies.AsNoTracking().FirstOrDefault(v => v.ExternalId.Equals(vacancy.ExternalId));

            if (vacancy.Department != null)
            {
                var persistedDepartment = _context.Departments.AsNoTracking().FirstOrDefault(d => d.ExternalId.Equals(vacancy.Department.ExternalId));

                if (persistedDepartment != null)
                {
                    _context.Entry(vacancy).Reference(v => v.Department).CurrentValue = persistedDepartment;
                    _context.Entry(vacancy).Property(v => v.DepartmentId).CurrentValue = persistedDepartment.Id;
                }

            }
            if (vacancy.Employer != null)
            {
                var persistedEmployer = _context.Employers.AsNoTracking().FirstOrDefault(d => d.ExternalId.Equals(vacancy.Employer.ExternalId));

                if (persistedEmployer != null)
                {
                    _context.Entry(vacancy).Reference(v => v.Employer).CurrentValue = persistedEmployer;
                    _context.Entry(vacancy).Property(v => v.EmployerId).CurrentValue = persistedEmployer.Id;
                }
            }

            if (persistedVacancy != null)
            {
                _context.Entry(vacancy).Property(v => v.Id).CurrentValue = persistedVacancy.Id;

                UpdateSpecializationLinks(persistedVacancy.VacancySpecializationLinks.ToList(),
                    vacancy.VacancySpecializationLinks.ToList());

                _context.Vacancies.Update(vacancy);
            }
            else
            {
                _context.Vacancies.Add(vacancy);
            }

            _context.SaveChanges();
        }

        public async Task<DataPage<Vacancy>> GetFilteredAsync(VacancyFilter filter)
        {
            try
            {
                return  await TryGetFilteredFromHeadHunter(filter);
            }
            catch (HttpRequestException e)
            {
                _logger.LogWarning($"HeadHunter.ru недоступен {e.Message}");
            }

            return await GetFilteredFromDatabase(filter);
        }


        private async Task<DataPage<Vacancy>> TryGetFilteredFromHeadHunter(VacancyFilter filter)
        {
            var vacanciesDataPage = new DataPage<Vacancy>();

            var headHunterFilter = new HeadHunterFilterJsonModel(filter);
            var queryString = await UrlHelper.ToQueryStringAsync(headHunterFilter);
            var httpResponse = await _httpClient.GetAsync($"vacancies?{queryString}");

            httpResponse.EnsureSuccessStatusCode();

            var sResult = await httpResponse.Content.ReadAsStringAsync();
            if (_jsonDeserializer.TryDeserializeContent(sResult, out var deserializedVacancies))
            {
                vacanciesDataPage = deserializedVacancies;
            }

            return vacanciesDataPage;
        }

        private async Task<DataPage<Vacancy>> GetFilteredFromDatabase(VacancyFilter filter)
        {
            var query = _context.Vacancies.AsNoTracking().AsQueryable();

            if (!String.IsNullOrWhiteSpace(filter.Text))
            {
                var predicate = PredicateBuilder.False<Vacancy>();

                if (!filter.SearchFields.Any() || filter.SearchFields.Contains(SearchField.CompanyName))
                {
                    predicate = predicate.Or(v => v.Employer.Name.Contains(filter.Text));
                }

                if (!filter.SearchFields.Any() || filter.SearchFields.Contains(SearchField.Name))
                {
                    predicate = predicate.Or(v => v.Name.Contains(filter.Text));
                }

                if (!filter.SearchFields.Any() || filter.SearchFields.Contains(SearchField.Description))
                {
                    predicate = predicate.Or(v => v.Description.Contains(filter.Text));
                }

                query = query.Where(predicate);

            }

            if (filter.EmploymentTypes.Any())
            {
                query = query.Where(v => filter.EmploymentTypes.Contains(v.Employment.Value));
            }

            if (filter.ScheduleTypes.Any())
            {
                query = query.Where(v => filter.ScheduleTypes.Contains(v.Schedule.Value));
            }

            if (filter.Experience.HasValue)
            {
                query = query.Where(v => v.Experience.Equals(filter.Experience));
            }

            if (filter.SpecializationExternalIds.Any())
            {
                foreach (var specialization in filter.SpecializationExternalIds)
                {
                    query = query.Where(v => v.VacancySpecializationLinks.Select(vsl => vsl.Specialization.ExternalId).Contains(specialization));
                }
            }

            if (filter.SalaryFilter != null)
            {
                if (filter.SalaryFilter.OnlyWithSalary)
                {
                    query = query.Where(v => v.Salary.From.HasValue || v.Salary.To.HasValue);
                }

                if (filter.SalaryFilter.Salary.HasValue)
                {
                    query = query.Where(v =>
                        (!v.Salary.From.HasValue || v.Salary.From.HasValue && ApplicationContext.ConvertToCurrency(v.Salary.From, filter.SalaryFilter.CurrencyCode) <= filter.SalaryFilter.Salary) &&
                        (!v.Salary.To.HasValue || v.Salary.To.HasValue && ApplicationContext.ConvertToCurrency(v.Salary.To, filter.SalaryFilter.CurrencyCode) >= filter.SalaryFilter.Salary));
                }
            }

            query = query.OrderBy(v => v.Id);

            var skip = filter.Page <= 1 ? 0 : (filter.Page - 1) * filter.PageSize;

            return new DataPage<Vacancy>
            {
                Count = await query.CountAsync(),
                Objects = await query.GetPaged(skip, filter.PageSize).ToListAsync(),
            };
        }

        private void UpdateSpecializationLinks(IReadOnlyList<VacancySpecializationLink> persistedVacancySpecializationLinks, IReadOnlyList<VacancySpecializationLink> currentVacancySpecializationLinks)
        {
            foreach (var vacancySpecializationLink in persistedVacancySpecializationLinks)
            {
                _context.Entry(vacancySpecializationLink).State = EntityState.Deleted;
            }

            foreach (var vacancySpecializationLink in currentVacancySpecializationLinks)
            {
                _context.Entry(vacancySpecializationLink).State = EntityState.Added;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.SearchModels;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class HeadHunterFilterJsonModel
    {
        public HeadHunterFilterJsonModel()
        {
        }

        public HeadHunterFilterJsonModel(VacancyFilter filter)
        {
            Text = filter.Text;
            SearchFields = filter.SearchFields.Select(sf => sf.ToHeadHunterFormat()).ToList();
            AreaExternalId = filter.AreaExternalId;
            Experience = filter.Experience.ToHeadHunterFormat();
            ScheduleTypes = filter.ScheduleTypes.Select(st => st.ToHeadHunterFormat()).ToList();
            MetroExternalId = filter.MetroExternalId;
            SpecializationsExternalId = filter.SpecializationExternalIds;
            EmployerExternalId = filter.EmployerExternalId;
            CurrencyCode = filter.SalaryFilter?.CurrencyCode;
            Salary = filter.SalaryFilter?.Salary;
            OnlyWithSalary = filter.SalaryFilter?.OnlyWithSalary ?? false;
            Period = filter.Period;
            DateFrom = filter.DateFrom;
            DateTo = filter.DateTo;
            Page = filter.Page;
            PageSize = filter.PageSize;
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("search_field")]
        public IReadOnlyList<string> SearchFields { get; set; }

        [JsonProperty("experience")]
        public string Experience { get; set; }

        [JsonProperty("schedule")]
        public IReadOnlyList<string> ScheduleTypes { get; set; }

        [JsonProperty("area")]
        public string AreaExternalId { get; set; }

        [JsonProperty("metro")]
        public string MetroExternalId { get; set; }

        [JsonProperty("specializations")]
        public IReadOnlyList<string> SpecializationsExternalId { get; set; }

        [JsonProperty("employer_id")]
        public string EmployerExternalId { get; set; }

        [JsonProperty("currency")]
        public string CurrencyCode { get; set; }

        [JsonProperty("salary")]
        public decimal? Salary { get; set; }

        [JsonProperty("only_with_salary")]
        public bool OnlyWithSalary { get; set; }

        [JsonProperty("period")]
        public int? Period { get; set; }

        [JsonProperty("date_from")]
        public DateTime? DateFrom { get; set; }

        [JsonProperty("date_to")]
        public DateTime? DateTo { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; } = 0;

        [JsonProperty("per_page")]
        public int PageSize { get; set; } = 50;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class VacancyJsonModel : IHeadHunterJsonModel<Vacancy>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("branded_description")]
        public string BrandedDescription { get; set; }

        [JsonProperty("key_skills")]
        public List<KeySkillJsonModel> KeySkills { get; set; } = new List<KeySkillJsonModel>();

        [JsonProperty("type")]
        public VacancyTypeJsonModel Type { get; set; }

        [JsonProperty("schedule")]
        public ScheduleJsonModel Schedule { get; set; }

        [JsonProperty("accept_handicapped")]
        public bool AcceptHandicapped { get; set; }

        [JsonProperty("accept_kids")]
        public bool AcceptKids { get; set; }

        [JsonProperty("experience")]
        public ExperienceJsonModel Experience { get; set; }

        [JsonProperty("address")]
        public AddressJsonModel AddressJsonModel { get; set; }

        [JsonProperty("alternate_url")]
        public Uri AlternateUrl { get; set; }

        [JsonProperty("apply_alternate_url")]
        public Uri ApplyAlternateUrl { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("department")]
        public DepartmentJsonModel Department { get; set; }

        [JsonProperty("employment")]
        public EmploymentJsonModel Employment { get; set; }

        [JsonProperty("salary")]
        public SalaryJsonModel SalaryJsonModel { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("area")]
        public AreaJsonModel AreaJsonModel { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("employer")]
        public EmployerJsonModel EmployerJsonModel { get; set; }

        [JsonProperty("response_url")]
        public object ResponseUrl { get; set; }

        [JsonProperty("has_test")]
        public bool HasTest { get; set; }

        [JsonProperty("test")]
        public TestJsonModel TestRequired { get; set; }

        [JsonProperty("specializations")]
        public List<SpecializationJsonModel> SpecializationsJsonModels { get; set; } = new List<SpecializationJsonModel>();

        [JsonProperty("contacts")]
        public ContactsJsonModel ContactsJsonModel { get; set; }

        [JsonProperty("driver_license_types")]
        public List<DriverLicenseTypeJsonModel> DriverLicenseTypes { get; set; }

        public Vacancy GetModel()
        {
            var vacancy = new Vacancy(
                Id,
                Name,
                Type.GetModel(),
                Description,
                KeySkills.Select(ks => ks.Name).ToList(),
                AddressJsonModel?.GetModel(),
                Schedule?.GetModel(),
                AcceptHandicapped,
                AcceptKids,
                Experience?.GetModel(),
                Employment?.GetModel(),
                SalaryJsonModel?.GetModel(),
                Archived,
                PublishedAt,
                Department?.GetModel(),
                ContactsJsonModel?.GetModel(),
                HasTest,
                TestRequired?.GetModel(),
                EmployerJsonModel.GetModel()
            );

            SpecializationsJsonModels.ForEach(s => vacancy.AddSpecialization(s.GetModel()));
            return vacancy;
        }

        public class SpecializationJsonModel : IHeadHunterJsonModel<Specialization>
        {
            [JsonProperty("profarea_id")]
            public string ProfAreaId { get; set; }

            [JsonProperty("profarea_name")]
            public string ProfAreaName { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            public Specialization GetModel()
            {
                return new Specialization(Id, Name, null, new ProfessionalArea(ProfAreaId, ProfAreaName));
            }
        }
    }
}
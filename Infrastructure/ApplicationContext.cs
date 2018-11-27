using System.Linq;
using Domain.Abstractions;
using Domain.Core;
using Domain.EntityLinks;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<ProfessionalArea> ProfessionalAreas { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<VacancySpecializationLink> VacancySpecializationLinks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyAllConfigurationsFromAssembly(GetType().Assembly);
        }

        [DbFunction]
        public static int ConvertToCurrency(decimal? amount, string currencyCode)
        {
            // Вычисления производятся скалярной функцией в базе данных
            return 42;
        }

        /// <summary>
        /// OwnsOne не могут быть опциональными, и всегда должны иметь значение https://github.com/aspnet/EntityFramework.Docs/issues/466
        /// </summary>
        /// <typeparam name="T">OwnsOne свойство (по факту все ValueObjects)</typeparam>
        /// <param name="entry">Entity который имеет этот ValueObject</param>
        private static void Fixup<T>(EntityEntry entry)
            where T : ValueObject
        {
            var type = typeof(T);
            if (entry.Reference(type.Name).CurrentValue == null)
            {
                entry.Reference(type.Name).CurrentValue = (T)type.GetMethod("Empty")?.Invoke(null, null);
            }

            entry.Reference(type.Name).TargetEntry.State = entry.State;
        }

        public override int SaveChanges()
        {
            var addedOrModifiedEntities = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified).ToList();

            foreach (var entry in addedOrModifiedEntities.Where(e => e.Entity is Employer))
            {
                Fixup<Logo>(entry);
            }
            foreach (var entry in addedOrModifiedEntities.Where(e => e.Entity is Vacancy))
            {
                Fixup<Address>(entry);
                Fixup<Contact>(entry);
                Fixup<Salary>(entry);
            }

            return base.SaveChanges();
        }
    }
}

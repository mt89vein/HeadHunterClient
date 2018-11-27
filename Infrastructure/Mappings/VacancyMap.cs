using Domain.Core;
using Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class VacancyMap : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> vacancy)
        {
            vacancy.Property(v => v.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            vacancy.HasKey(v => v.Id).ForSqlServerIsClustered();
            vacancy.HasIndex(v => v.ExternalId).IsUnique();
            vacancy.HasIndex(v => v.DepartmentId);
            vacancy.HasIndex(v => v.EmployerId);
            vacancy.HasOne(v => v.Department)
                .WithMany()
                .HasForeignKey(v => v.DepartmentId)
                .IsRequired(false);

            vacancy.HasOne(v => v.Employer)
                .WithMany()
                .HasForeignKey(v => v.EmployerId);

            var commaSeparateValueConverter = new CommaSeparateValueConverter<string>();
            vacancy.Property(v => v.KeySkills).HasConversion(commaSeparateValueConverter);

            vacancy.OwnsOne(v => v.Contact);
            vacancy.OwnsOne(v => v.Address);
            vacancy.OwnsOne(v => v.Salary);
            var vacancySpecializationNavigationProperty = vacancy.Metadata.FindNavigation(nameof(Vacancy.VacancySpecializationLinks));
            vacancySpecializationNavigationProperty.SetPropertyAccessMode(PropertyAccessMode.Field);
            vacancySpecializationNavigationProperty.IsCollection();
            vacancySpecializationNavigationProperty.IsEagerLoaded = true;
            vacancy.ToTable(nameof(Vacancy));
        }
    }
}
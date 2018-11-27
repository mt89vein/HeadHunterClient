using Domain.EntityLinks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class VacancySpecializationLinkMap : IEntityTypeConfiguration<VacancySpecializationLink>
    {
        public void Configure(EntityTypeBuilder<VacancySpecializationLink> vacancySpecializationLink)
        {
            vacancySpecializationLink.HasKey(vsl => new { vsl.SpecializationId, vsl.VacancyId });
            vacancySpecializationLink.HasOne(vsl => vsl.Vacancy)
                .WithMany(v => v.VacancySpecializationLinks)
                .HasForeignKey(vsl => vsl.VacancyId)
                .IsRequired();

            vacancySpecializationLink.HasOne(vsl => vsl.Specialization)
                .WithMany()
                .HasForeignKey(vsl => vsl.SpecializationId)
                .IsRequired();

            vacancySpecializationLink.HasIndex(vsl => vsl.SpecializationId);
            vacancySpecializationLink.HasIndex(vsl => vsl.VacancyId);
            vacancySpecializationLink.ToTable(nameof(VacancySpecializationLink));
        }
    }
}
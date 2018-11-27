using Domain.Core;
using Domain.EntityLinks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class SpecializationMap : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> specialization)
        {
            specialization.Property(s => s.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            specialization.HasKey(s => s.Id).ForSqlServerIsClustered();
            specialization.HasIndex(v => v.ExternalId).IsUnique();
            specialization.HasOne(s => s.ProfessionalArea)
                .WithMany(p => p.Specializations)
                .HasForeignKey(s => s.ProfessionalAreaId)
                .IsRequired();
            specialization.Metadata.FindNavigation(nameof(Specialization.ProfessionalArea)).IsEagerLoaded = true;
            specialization.HasIndex(s => s.ProfessionalAreaId);
            specialization.ToTable(nameof(Specialization));
        }
    }
}
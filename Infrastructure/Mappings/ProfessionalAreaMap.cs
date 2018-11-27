using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class ProfessionalAreaMap : IEntityTypeConfiguration<ProfessionalArea>
    {
        public void Configure(EntityTypeBuilder<ProfessionalArea> professionalArea)
        {
            professionalArea.Property(p => p.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            professionalArea.HasKey(p => p.Id).ForSqlServerIsClustered();
            professionalArea.HasIndex(v => v.ExternalId).IsUnique();
            professionalArea.HasMany(p => p.Specializations)
                .WithOne(s => s.ProfessionalArea)
                .HasForeignKey(s => s.ProfessionalAreaId)
                .IsRequired();
            professionalArea.Metadata.FindNavigation(nameof(ProfessionalArea.Specializations)).SetPropertyAccessMode(PropertyAccessMode.Field);
            professionalArea.ToTable(nameof(ProfessionalArea));
        }
    }
}
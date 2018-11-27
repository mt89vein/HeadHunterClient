using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class EmployerMap : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> employer)
        {
            employer.Property(e => e.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            employer.HasKey(e => e.Id).ForSqlServerIsClustered();
            employer.HasIndex(v => v.ExternalId).IsUnique();
            employer.OwnsOne(e => e.Logo);
            employer.ToTable(nameof(Employer));
        }
    }
}
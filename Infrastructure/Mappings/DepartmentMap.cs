using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> department)
        {
            department.Property(d => d.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            department.HasKey(d => d.Id).ForSqlServerIsClustered();
            department.HasIndex(v => v.ExternalId).IsUnique();
            department.HasMany<Vacancy>()
                .WithOne(w => w.Department)
                .HasForeignKey(w => w.DepartmentId)
                .IsRequired();
            department.ToTable(nameof(Department));
        }
    }
}
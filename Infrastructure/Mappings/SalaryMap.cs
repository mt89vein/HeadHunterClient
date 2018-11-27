using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class SalaryMap : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> salary)
        {
            salary.Property(c => c.From).HasColumnType("DECIMAL(18,6)");
            salary.Property(c => c.To).HasColumnType("DECIMAL(18,6)");
        }
    }
}
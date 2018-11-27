using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class AreaMap : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> area)
        {
            area.Property(a => a.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            area.HasKey(a => a.Id).ForSqlServerIsClustered();
            area.HasIndex(v => v.ExternalId).IsUnique();
            area.ToTable(nameof(Area));
        }
    }
}
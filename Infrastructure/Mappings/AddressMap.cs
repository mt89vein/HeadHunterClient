using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> address)
        {
            address.Property(c => c.Longitude).HasColumnType("DECIMAL(18,6)");
            address.Property(c => c.Latitude).HasColumnType("DECIMAL(18,6)");
        }
    }
}
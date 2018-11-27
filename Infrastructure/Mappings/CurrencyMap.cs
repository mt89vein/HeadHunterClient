using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class CurrencyMap : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> currency)
        {
            currency.Property(c => c.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            currency.HasKey(e => e.Id).ForSqlServerIsClustered();
            currency.Property(c => c.Rate).HasColumnType("DECIMAL(18,6)");
            currency.HasIndex(v => v.Code).IsUnique();
            currency.Ignore(v => v.ExternalId);
            currency.ToTable(nameof(Currency));
        }
    }
}
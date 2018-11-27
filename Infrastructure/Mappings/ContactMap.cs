using System.Collections.Generic;
using Domain.Core;
using Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> contact)
        {
            var jsonConverter = new JsonValueConverter<PhoneNumber>();
            contact.Property(c => c.PhoneNumbers).HasConversion(jsonConverter);
        }
    }
}
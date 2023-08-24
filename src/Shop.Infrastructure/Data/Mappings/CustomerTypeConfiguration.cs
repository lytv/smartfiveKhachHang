using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities.CustomerTypeAggregate;
using Shop.Infrastructure.Data.Extensions;

namespace Shop.Infrastructure.Data.Mappings;
public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerType>
{
    public void Configure(EntityTypeBuilder<CustomerType> builder)
    {
        builder.ConfigureBaseEntity();

        builder
            .Property(customer => customer.CustomerTypeCode)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(10);

        builder
            .Property(customer => customer.Description)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(255);

        builder
            .Property(customer => customer.TenantId)
            .IsRequired() // NOT NULL
            .IsUnicode(false); // VARCHAR
    }
}

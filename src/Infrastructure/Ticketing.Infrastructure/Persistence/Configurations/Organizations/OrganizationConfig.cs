using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Organizations;

namespace Ticketing.Infrastructure.Persistence.Configurations.Organizations;
public class OrganizationConfig : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations").HasKey(_ => _.Id);

        builder.Property(_ => _.Id).IsRequired();

        builder.Property(_ => _.Name).IsRequired();

        builder.Property(_ => _.IsDefault);

        builder.Property(_ => _.OrganizationCode).IsRequired();

        builder.HasIndex(c => c.OrganizationCode).IsUnique();

    }
}

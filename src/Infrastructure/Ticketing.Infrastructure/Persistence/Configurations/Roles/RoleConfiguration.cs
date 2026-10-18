using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Roles;

namespace Ticketing.Infrastructure.Persistence.Configurations.Roles;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(_ => _.Id);
       
        builder.Property(_ => _.Title).IsRequired();
        
        builder.Property(_ => _.Description).IsRequired(false);
    }
}

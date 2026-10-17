using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Users;

namespace Ticketing.Infrastructure.Persistence.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(t => t.Id);

        builder.Property(_ => _.FirstName).IsRequired();

        builder.Property(_ => _.LastName).IsRequired();

        builder.Property(_ => _.Mobile).IsRequired();

        builder.Property(_ => _.HashPassword).IsRequired();

        builder.Property(_ => _.CreatedAt);
    }
}

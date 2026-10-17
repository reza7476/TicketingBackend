using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.UserRoles;

namespace Ticketing.Infrastructure.Persistence.Configurations.UserRoles;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles").HasKey(t => t.Id);

        builder.Property(_ => _.UserId).IsRequired();

        builder.Property(_ => _.RoleId).IsRequired();

        builder.HasOne(_ => _.User)
            .WithMany(_ => _.Roles)
            .HasForeignKey(_ => _.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(_ => _.Role)
            .WithMany(_ => _.Roles)
            .HasForeignKey(_ => _.RoleId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

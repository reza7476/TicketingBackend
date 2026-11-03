using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Users;

namespace Ticketing.Infrastructure.Persistence.Configurations.UserDocuments;

public class UserDocumentConfiguration : IEntityTypeConfiguration<UserDocument>
{
    public void Configure(EntityTypeBuilder<UserDocument> builder)
    {
        builder.ToTable("UserDocuments").HasKey(_ => _.Id);
        builder.Property(_ => _.DocumentId).IsRequired();
        builder.Property(_ => _.UserId).IsRequired();
        builder.Property(_ => _.Extension).IsRequired();

        builder.HasOne(_ => _.User)
            .WithMany(_ => _.Documents)
            .HasForeignKey(_ => _.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

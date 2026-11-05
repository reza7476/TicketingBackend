using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.TicketReplies;

namespace Ticketing.Infrastructure.Persistence.Configurations.TicketReplyDocuments;

public class ReplyDocumentConfiguration : IEntityTypeConfiguration<ReplyDocuments>
{
    public void Configure(EntityTypeBuilder<ReplyDocuments> builder)
    {
        builder.ToTable("ReplyDocuments").HasKey(_ => _.Id);
        
        builder.Property(_ => _.Id).IsRequired();

        builder.Property(_ => _.DocumentId).IsRequired();

        builder.Property(_=>_.Extension).IsRequired();

        builder.Property(_=>_.ReplyId).IsRequired();

        builder.HasOne(_ => _.Reply)
            .WithMany(_ => _.Documents)
            .HasForeignKey(_ => _.ReplyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

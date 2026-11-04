using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Infrastructure.Persistence.Configurations.TicketDocuments;

public class TicketDocumentConfiguration : IEntityTypeConfiguration<TicketDocument>
{
    public void Configure(EntityTypeBuilder<TicketDocument> builder)
    {
        builder.ToTable("TicketDocuments").HasKey(t => t.Id);

        builder.Property(_ => _.Id).IsRequired();
        builder.Property(_ => _.DocumentId).IsRequired();
        builder.Property(_ => _.Extension).IsRequired();
        builder.Property(_ => _.TicketId).IsRequired();

        builder
            .HasOne(t => t.Ticket)
            .WithMany(_ => _.Documents)
            .HasForeignKey(t => t.TicketId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.TicketReplies;

namespace Ticketing.Infrastructure.Persistence.Configurations.TicketReplies;

public class TicketReplyConfiguration : IEntityTypeConfiguration<TicketReply>
{
    public void Configure(EntityTypeBuilder<TicketReply> builder)
    {
        builder.ToTable("TicketReplies").HasKey(_ => _.Id);

        builder.Property(_ => _.Id).IsRequired();

        builder.Property(_=>_.TicketId).IsRequired();

        builder.Property(_=>_.UserId).IsRequired();

        builder.Property(_=>_.Content).IsRequired();

        builder.Property(_=>_.CreatedAt).IsRequired();

        builder.HasOne(_=>_.Ticket)
          .WithMany(_=>_.Replies)
          .HasForeignKey(_=>_.TicketId)
          .OnDelete(DeleteBehavior.Cascade);
    }
}

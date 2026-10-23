using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.TicketAssignments;

namespace Ticketing.Infrastructure.Persistence.Configurations.TicketAssignments;

public class TicketAssignmentConfiguration : IEntityTypeConfiguration<TicketAssignment>
{
    public void Configure(EntityTypeBuilder<TicketAssignment> builder)
    {
        builder.ToTable("TicketAssignments").HasKey(t => t.Id);

        builder.Property(_ => _.Id).IsRequired();

        builder.Property(_ => _.TicketId).IsRequired();

        builder.Property(_ => _.AssignedToUserId).IsRequired();

        builder.Property(_ => _.CreatedAt).IsRequired();

        builder.HasOne(_ => _.Ticket)
            .WithMany(_ => _.TicketAssignments)
            .HasForeignKey(_ => _.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Infrastructure.Persistence.Configurations.Tickets;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets").HasKey(t => t.Id);
        
        builder.Property(_ => _.Id).IsRequired();
        
        builder.Property(_=>_.UserId).IsRequired();
        
        builder.Property(_=>_.Title).IsRequired();
        
        builder.Property(_=>_.Content).IsRequired();
        
        builder.Property(_=>_.Priority).IsRequired();
        
        builder.Property(_=>_.Status).IsRequired();
        
        builder.Property(_ => _.CreatedAt);
        builder.Property(t => t.Number)
             .UseIdentityByDefaultColumn()
             .IsRequired();

        builder.HasIndex(t => t.Number)
            .IsUnique();
    }
}

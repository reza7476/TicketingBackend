using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Documents;

namespace Ticketing.Infrastructure.Persistence.Configurations.Documents;

public class DocumentConfig : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents").HasKey(e => e.Id);
        builder.Property(_ => _.ObjectName).IsRequired();
        builder.Property(_ => _.ContentType).IsRequired();
        builder.Property(_ => _.Size);
        builder.Property(_ => _.OriginalFileName).IsRequired();
        builder.Property(_ => _.CreatedAt);
    }
}

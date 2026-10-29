namespace Ticketing.Domain.Entities.Documents;

public class Document
{
    public string Id { get; set; } = default!;
    public string ObjectName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long Size { get; set; }
    public string OriginalFileName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}

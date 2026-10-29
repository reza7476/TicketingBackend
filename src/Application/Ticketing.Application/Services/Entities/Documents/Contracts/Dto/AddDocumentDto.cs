namespace Ticketing.Application.Services.Entities.Documents.Contracts.Dto;

public class AddDocumentDto
{
    public string Id { get; set; } = default!;
    public string ObjectName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long Size { get; set; }
    public string OriginalFileName { get; set; } = default!;
}

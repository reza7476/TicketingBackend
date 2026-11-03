namespace Ticketing.Shared.DTOS;

public class DownloadDocumentDto
{
    public byte[] Data { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string FileName { get; set; } = default!;
}

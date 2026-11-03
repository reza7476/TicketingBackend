using Ticketing.Shared.DTOS;

namespace Ticketing.Shared.Interfaces.Storage;

public interface IFileStorage
{
    Task Delete(string id);
    Task<DownloadDocumentDto?> Download(string id);
    Task<DocumentInfoDto> Upload(
        byte[] fileAsByte,
        string fileExtension,
        string contentType,
        string originalFileName);
}

using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Ticketing.Application.Services.Entities.Documents.Contracts;
using Ticketing.Application.Services.Entities.Documents.Contracts.Dto;
using Ticketing.Shared.DTOS;
using Ticketing.Shared.Interfaces.Storage;

namespace Ticketing.Infrastructure.Services.Storage;

public class MinIOAppService : IFileStorage
{

    private readonly IMinioClient _client;
    private readonly IOptions<MiIoSettingsDto> _setting;

    private readonly IDocumentService _documentService;

    public MinIOAppService(
        IMinioClient client,
         IOptions<MiIoSettingsDto> setting,
        IDocumentService documentService)
    {
        _client = client;
        _setting = setting;
        _documentService = documentService;
    }

    public Task Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<DownloadDocumentDto?> Download(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<DocumentInfoDto> Upload(
       byte[] file,
       string fileExtension,
       string contentType,
       string originalFileName)
    {
        var objectName = Guid.NewGuid().ToString();
        var fileStream = new MemoryStream(file);
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_setting.Value.BucketName)
            .WithObject(objectName)
            .WithStreamData(fileStream)
            .WithObjectSize(file.Length)
            .WithContentType(contentType);
        await _client.PutObjectAsync(putObjectArgs);
        await _documentService.Add(new AddDocumentDto()
        {
            Id = objectName,
            ObjectName = objectName,
            ContentType = contentType,
            Size = file.Length,
            OriginalFileName = originalFileName
        });
        var result = new DocumentInfoDto()
        {
            DocumentId = objectName,
            Extension = contentType
        };

        return result;
    }
}

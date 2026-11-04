using Microsoft.AspNetCore.Mvc;
using Ticketing.Shared.DTOS;
using Ticketing.Shared.Interfaces.Storage;

namespace Ticketing.Api.Controllers.Storage;

[Route("api/storages")]
[ApiController]
public class StoragesController : ControllerBase
{

    private readonly IFileStorage _service;

    public StoragesController(IFileStorage fileService)
    {
        _service = fileService;
    }


    [HttpPost("upload")]
    public async Task<DocumentInfoDto> Upload([FromForm]IFormFile file)
    {
        const long maxFileSize = 10 * 1024 * 1024;
        if (file == null || file.Length == 0)
        {
            throw new Exception("File is empty");
        }
        if (file.Length > maxFileSize)
        {
            throw new Exception("File size exceeds 10MB limit");
        }

        var fileAsByte = await FormFileToByteArrayAsync(file);
        var fileExtension = Path.GetExtension(file.FileName);
        var originalFileName = file.FileName;
        return await _service.Upload(fileAsByte, fileExtension, file.ContentType, originalFileName);
    }


    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download([FromRoute] string id)
    {
        var result= await _service.Download(id);
        if (result == null)
            return  NoContent();
        return File(result.Data, result.ContentType, result.FileName);
    }



    private async Task<byte[]> FormFileToByteArrayAsync(IFormFile file)
    {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileStream = memoryStream.ToArray();
        await memoryStream.FlushAsync();

        return fileStream;
    }
}

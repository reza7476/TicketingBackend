using Ticketing.Application.Services.Entities.Documents.Contracts;
using Ticketing.Application.Services.Entities.Documents.Contracts.Dto;
using Ticketing.Domain.Entities.Documents;
using Ticketing.Shared.Exceptions;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.Documents;

public class DocumentAppService : IDocumentService
{

    private readonly IDocumentRepository _repository;
    private readonly IDateTimeService _dataTimeService;
    private readonly IUnitOfWork _unitOfWork;

    public DocumentAppService(
        IDocumentRepository repository,
        IDateTimeService dataTimeService,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _dataTimeService = dataTimeService;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddDocumentDto dto)
    {
        var document = new Document()
        {
            Id = dto.Id,
            ObjectName = dto.ObjectName,
            OriginalFileName = dto.OriginalFileName,
            ContentType = dto.ContentType,
            Size = dto.Size,
            CreatedAt = _dataTimeService.Now,
        };

        await _repository.Add(document);
        await _unitOfWork.Complete();
    }

    public async Task<Document?> FindById(string id)
    {
        return await _repository.FindById(id);
    }

    public async Task Remove(string documentId)
    {
      var document=await _repository.FindById(documentId);

        if (document == null)
        {
            throw new DocumentNotFoundException();
        }

        await _repository.Remove(document);
    }
}

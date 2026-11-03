using Ticketing.Application.Services.Entities.Documents.Contracts.Dto;
using Ticketing.Domain.Entities.Documents;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Documents.Contracts;

public interface IDocumentService : IService
{
    Task Add(AddDocumentDto dto);
    Task<Document?> FindById(string id);
}

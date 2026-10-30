using Ticketing.Application.Services.Entities.Documents.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Documents.Contracts;

public interface IDocumentService : IService
{
    Task<string> Add(AddDocumentDto addDocumentDto);
}

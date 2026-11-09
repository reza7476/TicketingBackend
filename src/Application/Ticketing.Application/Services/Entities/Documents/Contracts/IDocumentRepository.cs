using Ticketing.Domain.Entities.Documents;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Documents.Contracts;

public interface IDocumentRepository : IRepository
{
    Task Add(Document document);
    Task<Document?> FindById(string id);
    Task Remove(Document document);
}

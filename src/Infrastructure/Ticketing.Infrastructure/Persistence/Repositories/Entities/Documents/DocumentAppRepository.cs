using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Documents.Contracts;
using Ticketing.Domain.Entities.Documents;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Documents;

public class DocumentAppRepository : IDocumentRepository
{

    private readonly DbSet<Document> _documents;

    public DocumentAppRepository(AppDbContext context)
    {
        _documents = context.Set<Document>();
    }

    public async Task Add(Document document)
    {
        await _documents.AddAsync(document);
    }

    public async Task<Document?> FindById(string id)
    {
        return await _documents.FindAsync(id);
    }

    public async Task Remove(Document document)
    {
        _documents.Remove(document);
        await Task.CompletedTask;
    }
}

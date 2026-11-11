using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Domain.Entities.TicketReplies;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.TicketReplies;

public class TicketReplyRepository : ITicketReplyRepository
{

    private readonly DbSet<TicketReply> _ticketReplies;
    private readonly DbSet<Ticket> _tickets;
    private readonly DbSet<ReplyDocuments> _replyDocuments;

    public TicketReplyRepository(AppDbContext context)
    {
        _ticketReplies = context.Set<TicketReply>();
        _tickets = context.Set<Ticket>();
        _replyDocuments = context.Set<ReplyDocuments>();
    }

    public async Task Add(TicketReply reply)
    {
        await _ticketReplies.AddAsync(reply);
    }

    public async Task DeleteReplyDocument(ReplyDocuments document)
    {
        _replyDocuments.Remove(document);
        await Task.CompletedTask;
    }

    public async Task<TicketReply?> FindById(string id)
    {
        return await _ticketReplies.FindAsync(id);
    }

    public async Task<TicketReply?> FindByIdWithDocument(string replyId)
    {
        return await _ticketReplies
            .Include(_ => _.Documents)
            .Where(_ => _.Id == replyId)
            .FirstOrDefaultAsync();
    }

    public async Task<ReplyDocuments?> FindDocumentById(string documentId)
    {
        return await _replyDocuments
            .Where(_ => _.DocumentId == documentId)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistedById(string id)
    {
        return await _ticketReplies.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> TicketIsExistById(string ticketId)
    {
        return await _tickets.AnyAsync(_ => _.Id == ticketId);
    }
}

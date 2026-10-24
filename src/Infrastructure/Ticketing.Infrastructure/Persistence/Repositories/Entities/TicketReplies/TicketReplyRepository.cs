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

    public TicketReplyRepository(AppDbContext context)
    {
        _ticketReplies = context.Set<TicketReply>();
        _tickets = context.Set<Ticket>();   
    }

    public async Task Add(TicketReply reply)
    {
        await _ticketReplies.AddAsync(reply);
    }

    public async Task<TicketReply?> FindById(string id)
    {
        return await _ticketReplies.FindAsync(id);
    }

    public async Task<bool> TicketIsExistById(string ticketId)
    {
       return await _tickets.AnyAsync(_=>_.Id== ticketId);  
    }
}

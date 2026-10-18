using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Domain.Entities.TicketReplies;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.TicketReplies;

public class TicketReplyRepository : ITicketReplyRepository
{

    private readonly DbSet<TicketReply> _ticketReplies;

    public TicketReplyRepository(AppDbContext context)
    {
        _ticketReplies = context.Set<TicketReply>();
    }

    public async Task Add(TicketReply reply)
    {
        await _ticketReplies.AddAsync(reply);
    }
}

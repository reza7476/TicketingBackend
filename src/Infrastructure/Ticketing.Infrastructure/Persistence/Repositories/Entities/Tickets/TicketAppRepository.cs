using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Tickets;

public class TicketAppRepository : ITicketRepository
{

    private readonly DbSet<Ticket> _tickets;

    public TicketAppRepository(AppDbContext context)
    {
        _tickets=context.Set<Ticket>();
    }

    public async Task Add(Ticket ticket)
    {
        await _tickets.AddAsync(ticket);
    }
}

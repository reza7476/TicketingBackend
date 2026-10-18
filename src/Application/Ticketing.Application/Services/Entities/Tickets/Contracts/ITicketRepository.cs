using Ticketing.Domain.Entities.Tickets;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketRepository : IRepository
{
    Task Add(Ticket ticket);
}

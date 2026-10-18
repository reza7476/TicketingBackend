using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketRepository : IRepository
{
    Task Add(Ticket ticket);
    
    Task<IPageResult<GetAllTicketsDto>> GetAllForAgent(
        IPagination? pagination,
        TicketFilterDto? filter);
    Task<GetTicketDetailsDto?> GetDetailsForAgent(string id);
}

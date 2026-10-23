using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketRepository : IRepository
{
    Task Add(Ticket ticket);
    Task<Ticket?> FindById(string id);
    Task<List<Ticket>> GetAllAnsweredTickets();
    Task<IPageResult<GetAllTicketsDto>> GetAllForAdmin(
        IPagination? pagination,
        TicketFilterDto? filter,
        string? search);
    
    Task<IPageResult<GetAllTicketsDto>> GetAllForClient(
        string userId,
        TicketFilterDto? filter,
        IPagination? pagination);
    
    Task<GetTicketDetailsDto?> GetDetailsForAgent(string id);
    Task<GetTicketDetailsDto?> GetDetailsForClient(string id, string userId);
}

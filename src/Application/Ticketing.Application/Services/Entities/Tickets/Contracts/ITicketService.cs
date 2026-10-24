using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketService : IService
{
    Task<string> Add(AddTicketDto dto, string? userId);
    Task AssignToAgent(AssignTicketToAgentDto dto);
    Task ChangeStatusByClient(ChangeTicketStatusByClientDto dto);
    Task ChangeStatusIntoAnswered(string ticketId);
    Task ChangeStatusIntoInProgress(string ticketId);
    Task ChangeStatusIntoOpen(string ticketId, string roleId);
    Task<IPageResult<GetAllAgentTicketsDto>> GetAllAgentTickets(
        string userId,
        IPagination? pagination,
        TicketFilterDto? filter);
    
    Task<IPageResult<GetAllTicketsDto>> GetAllForAdmin(
        IPagination? pagination = null,
        TicketFilterDto? filter = null,
        string? search = null);
    Task<IPageResult<GetAllTicketsDto>> GetAllForClient(
        string userId,
        TicketFilterDto? filter,
        IPagination? pagination);
    Task<GetTicketDetailsForAgentDto?> GetDetailsForAgent(string id);
    Task<GetTicketDetailsDto?> GetDetailsForAmin(string id);
    Task<GetClientTicketDetailsDto?> GetDetailsForClient(string id, string userId);
}

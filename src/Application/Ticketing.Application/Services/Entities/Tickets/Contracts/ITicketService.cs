using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketService : IService
{
    Task<string> Add(AddTicketDto dto, string? userId);
    Task ChangeStatusByAgent(ChangeTicketStatusDto dto);
    Task ChangeStatusByClient(ChangeTicketStatusByClientDto dto);
    Task ChangeStatusIntoAnswered(string ticketId);
    Task<IPageResult<GetAllTicketsDto>> GetAllForAdmin(
        IPagination? pagination = null,
        TicketFilterDto? filter = null,
        string? search = null);
    Task<IPageResult<GetAllTicketsDto>> GetAllForClient(
        string userId,
        TicketFilterDto? filter,
        IPagination? pagination);
    
    Task<GetTicketDetailsDto?> GetDetailsForAgent(string id);
    Task<GetTicketDetailsDto?> GetDetailsForClient(string id, string userId);
}

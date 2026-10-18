using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketService : IService
{
    Task<string> Add(AddTicketDto dto, string? userId);
  
    Task<IPageResult<GetAllTicketsDto>> GetAllForAgent(
        IPagination? pagination=null,
        TicketFilterDto? filter = null);
}

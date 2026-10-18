using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts;

public interface ITicketService : IService
{
    Task<string> Add(AddTicketDto dto, string? userId);

}

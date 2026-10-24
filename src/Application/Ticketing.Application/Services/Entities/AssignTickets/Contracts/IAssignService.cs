using Ticketing.Application.Services.Entities.AssignTickets.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.AssignTickets.Contracts;

public interface IAssignService : IService
{
    Task Add(AddAssignTicketDto dto);
}

using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.UseCases.AssignTickets.Contracts;

public interface IAssignHandler : IService
{
    Task AssignToAgent(AssignTicketToAgentDto dto);
}

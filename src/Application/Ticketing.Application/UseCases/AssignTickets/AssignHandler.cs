using Ticketing.Application.Services.Entities.AssignTickets.Contracts;
using Ticketing.Application.Services.Entities.AssignTickets.Contracts.Dto;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Application.UseCases.AssignTickets.Contracts;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.UseCases.AssignTickets;

public class AssignHandler : IAssignHandler
{

    private readonly ITicketService _ticketService;
    private readonly IAssignService _assignService;
    private readonly IUnitOfWork _unitOfWork;

    public AssignHandler(
        ITicketService ticketService,
        IAssignService assignService,
        IUnitOfWork unitOfWork)
    {
        _ticketService = ticketService;
        _assignService = assignService;
        _unitOfWork = unitOfWork;
    }

    public async Task AssignToAgent(AssignTicketToAgentDto dto)
    {
        await _unitOfWork.Begin();
        try
        {

            await _ticketService.ChangeStatusIntoInProgress(dto.TicketId);
            await _assignService.Add(new AddAssignTicketDto()
            {
                AgentId = dto.AgentId,
                TicketId = dto.TicketId,    
            });
            await _unitOfWork.Commit(); 

        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }

    }
}

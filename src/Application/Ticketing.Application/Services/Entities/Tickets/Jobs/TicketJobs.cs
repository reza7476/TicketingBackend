using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Domain.Entities.Tickets;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.Tickets.Jobs;

public class TicketJobs
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketRepository _ticketRepository;

    public TicketJobs(
        IUnitOfWork unitOfWork,
        ITicketRepository ticketRepository)
    {
        _unitOfWork = unitOfWork;
        _ticketRepository = ticketRepository;
    }

    public async Task ChangeTicketStatus()
    {
        var tickets = await _ticketRepository.GetAllAnsweredTickets();

        foreach (var ticket in tickets)
        {
            ticket.Status = TicketStatus.Closed;
        }
        await _unitOfWork.Complete();
    }
}

namespace Ticketing.Application.Services.Entities.AssignTickets.Contracts.Dto;

public class AddAssignTicketDto
{
    public string TicketId { get; set; } = default!;
    public string AgentId { get; set; } = default!;
}

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class AssignTicketToAgentDto
{
    public string TicketId { get; set; } = default!;
    public string AgentId { get; set; }  =default!;
}

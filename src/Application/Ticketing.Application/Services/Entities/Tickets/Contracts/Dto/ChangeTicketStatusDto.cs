using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class ChangeTicketStatusDto
{
    public string TicketId { get; set; } = default!;
    public TicketStatus Status { get; set; }
}

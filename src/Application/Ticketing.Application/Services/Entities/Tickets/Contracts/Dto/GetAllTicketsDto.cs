using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class GetAllTicketsDto
{
    public string Id { get; set; } = default!;
    public string AskedBy { get; set; } = default!;
    public string Title { get; set; }=default!;
    public TicketStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
}

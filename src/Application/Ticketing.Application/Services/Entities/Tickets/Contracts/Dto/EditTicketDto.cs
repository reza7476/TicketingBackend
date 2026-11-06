using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class EditTicketDto
{
    public string TicketId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Priority Priority { get; set; }
}

using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Domain.Entities.TicketAssignments;

public class TicketAssignment
{
    public string Id { get; set; } = default!;
    public string TicketId { get; set; } = default!;
    public string AssignedToUserId { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Ticket Ticket { get; set; }= default!;
}

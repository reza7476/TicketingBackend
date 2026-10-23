using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Domain.Entities.TicketReplies;

public class TicketReply
{

    public string Id { get; set; } = default!;
    public string TicketId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Content { get; set; } = default!;
    public bool ApprovedByAdmin { get; set; }
    public string IssuedBy { get; set; } = default!;//agent-client-admin
    public DateTime CreatedAt { get; set; }
    public Ticket Ticket { get; set; } = default!;

}

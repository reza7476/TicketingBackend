using Ticketing.Domain.Entities.TicketReplies;

namespace Ticketing.Domain.Entities.Tickets;

public class Ticket
{
    public Ticket()
    {
        Replies = new List<TicketReply>();
    }

    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Priority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TicketReply> Replies { get; set; }

}


public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2,
}

public enum TicketStatus
{
    Open = 0,
    InProgress = 1,
    Answered = 2,
    Closed = 3
}


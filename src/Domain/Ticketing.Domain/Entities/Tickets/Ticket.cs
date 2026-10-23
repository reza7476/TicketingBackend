using Ticketing.Domain.Entities.TicketAssignments;
using Ticketing.Domain.Entities.TicketReplies;

namespace Ticketing.Domain.Entities.Tickets;

public class Ticket
{
    public Ticket()
    {
        Replies = new List<TicketReply>();
        TicketAssignments = new List<TicketAssignment>();
    }

    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Priority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public long Number { get; set; }
    public List<TicketReply> Replies { get; set; }
    public List<TicketAssignment> TicketAssignments { get; set; }

}


public enum Priority
{
    Low = 1,
    Medium = 2,
    High = 3,
}

public enum TicketStatus
{
    Open = 1,
    InProgress = 2,
    Answered = 3,
    Closed = 4
}


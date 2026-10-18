using System.Reflection;
using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class GetTicketDetailsDto
{
    public string Content { get; set; } = default!;
    public TicketStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Priority Priority { get; set; }
    public string AskedBy { get; set; } = default!;
    public string Title { get; set; } = default!;

    public List<TicketRepliedDto> Replies { get; set; } = new();

}


public class TicketRepliedDto
{
    public DateTime RepliedAt { get; set; }
    public string RepliedBy { get; set; }= default!;
    public string Content { get; set; } = default!;
    public bool AgentReplied { get; set; }

}



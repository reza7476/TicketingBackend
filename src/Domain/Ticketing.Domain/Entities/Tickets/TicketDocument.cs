namespace Ticketing.Domain.Entities.Tickets;

public class TicketDocument
{
    public string Id { get; set; } = default!;
    public string DocumentId { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string TicketId { get; set; } = default!;
    public Ticket Ticket { get; set; } = default!;

}

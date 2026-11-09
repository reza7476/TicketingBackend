namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class RemoveDocumentDto
{
    public string DocumentId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string TicketId { get; set; } = default!;
}

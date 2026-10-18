namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class ChangeTicketStatusByClientDto
{
    public string UserId { get; set; } = default!;
    public string TicketId { get; set; } = default!;
}

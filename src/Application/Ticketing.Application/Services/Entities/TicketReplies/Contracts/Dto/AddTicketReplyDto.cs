namespace Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;

public class AddTicketReplyDto
{
    public string TicketId { get; set; } = default!;
    public string Content { get; set; } = default!;


}

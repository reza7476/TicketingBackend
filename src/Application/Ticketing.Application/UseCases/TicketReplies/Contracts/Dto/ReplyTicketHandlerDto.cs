using System.ComponentModel.DataAnnotations;

namespace Ticketing.Application.UseCases.TicketReplies.Contracts.Dto;

public class ReplyTicketHandlerDto
{
    [Required]
    public string TicketId { get; set; } = default!;

    [Required]
    public string Content { get; set; } = default!;
}

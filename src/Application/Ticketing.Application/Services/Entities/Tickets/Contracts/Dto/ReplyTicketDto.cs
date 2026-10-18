using System.ComponentModel.DataAnnotations;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class ReplyTicketDto
{
    [Required]
    public string TicketId { get; set; } = default!;

    [Required]
    public string Content { get; set; } = default!;

}

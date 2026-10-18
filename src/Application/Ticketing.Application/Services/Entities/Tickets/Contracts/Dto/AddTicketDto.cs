using System.ComponentModel.DataAnnotations;
using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class AddTicketDto
{
    [Required]
    public string Title { get; set; } = default!;


    [Required]
    public string Content { get; set; } = default!;

    [Required]
    public Priority Priority { get; set; } = default!;

}

using Ticketing.Domain.Entities.Tickets;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class TicketFilterDto
{
    public Priority priority { get; set; }
}

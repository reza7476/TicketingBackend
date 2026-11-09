using Ticketing.Application.Services.Entities.Documents.Contracts.Dto;

namespace Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;

public class AddTicketFileDto
{
    public string Id { get; set; } = default!;
    public DocumentDto Document { get; set; } = default!;
}

using Ticketing.Application.Services.Entities.Documents.Contracts.Dto;

namespace Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;

public class AddReplyDocumentDto
{
    public string ReplyId { get; set; } = default!;
    public DocumentDto Document { get; set; } = default!;
}

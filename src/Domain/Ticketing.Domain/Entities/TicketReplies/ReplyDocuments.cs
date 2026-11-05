namespace Ticketing.Domain.Entities.TicketReplies;

public class ReplyDocuments
{
    public string Id { get; set; } = default!;
    public string DocumentId { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string ReplyId { get; set; } = default!;
    public TicketReply Reply { get; set; } = default!;
}

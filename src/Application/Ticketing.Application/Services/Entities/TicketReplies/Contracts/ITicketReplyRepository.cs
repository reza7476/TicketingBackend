using Ticketing.Domain.Entities.TicketReplies;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.TicketReplies.Contracts;

public interface ITicketReplyRepository : IRepository
{
    Task Add(TicketReply reply);
    Task DeleteReplyDocument(ReplyDocuments document);
    Task<TicketReply?> FindById(string id);
    Task<TicketReply?> FindByIdWithDocument(string replyId);
    Task<ReplyDocuments?> FindDocumentById(string documentId);
    Task<bool> IsExistedById(string replyId);
    Task<bool> TicketIsExistById(string ticketId);
}

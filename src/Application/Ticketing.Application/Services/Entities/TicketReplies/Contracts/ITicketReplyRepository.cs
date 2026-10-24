using Ticketing.Domain.Entities.TicketReplies;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.TicketReplies.Contracts;

public interface ITicketReplyRepository : IRepository
{
    Task Add(TicketReply reply);
    Task<TicketReply?> FindById(string id);
    Task<bool> TicketIsExistById(string ticketId);
}

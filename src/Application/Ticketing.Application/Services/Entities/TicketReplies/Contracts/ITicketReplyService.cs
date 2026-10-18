using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.TicketReplies.Contracts;

public interface ITicketReplyService : IService
{
    Task<string> AddReply(AddTicketReplyDto dto);
}

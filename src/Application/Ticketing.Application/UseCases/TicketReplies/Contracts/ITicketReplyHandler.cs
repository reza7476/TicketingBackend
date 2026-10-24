using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Application.UseCases.TicketReplies.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.UseCases.TicketReplies.Contracts;

public interface ITicketReplyHandler : IService
{
    Task Approve(ApproveReplyDto dto);
 
}

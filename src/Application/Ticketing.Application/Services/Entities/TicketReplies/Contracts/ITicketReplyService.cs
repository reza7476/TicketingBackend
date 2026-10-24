using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.TicketReplies.Contracts;

public interface ITicketReplyService : IService
{
    Task<string> Add(
        AddTicketReplyDto dto,
        string userId,
        string userRole);
    Task<string> Approve(string id);
    Task Edit(string id, EditTicketReplyDto dto);
}

using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.UseCases.TicketReplies.Contracts;
using Ticketing.Application.UseCases.TicketReplies.Contracts.Dto;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.UseCases.TicketReplies;

public class TicketReplyHandler : ITicketReplyHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketService _ticketService;
    private readonly ITicketReplyService _ticketReplyService;


    public TicketReplyHandler(
        IUnitOfWork unitOfWork,
        ITicketService ticketService,
        ITicketReplyService ticketReplyService)
    {
        _unitOfWork = unitOfWork;
        _ticketService = ticketService;
        _ticketReplyService = ticketReplyService;
    }

    public async Task<string> ReplyTicketByAgentAsync(ReplyTicketHandlerDto dto, string userId)
    {
        await _unitOfWork.Begin();
        try
        {
            await _ticketService.ChangeStatusIntoAnswered(dto.TicketId);
            var replyTicketId = await _ticketReplyService
                .AddReply(new AddTicketReplyDto()
                {
                    Content = dto.Content,
                    TicketId = dto.TicketId,
                    UserId = userId
                });
            await _unitOfWork.Commit();

            return replyTicketId;

        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }
    }
}

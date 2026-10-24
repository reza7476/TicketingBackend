using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.UseCases.TicketReplies.Contracts;
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

    public async Task<string> Add(AddTicketReplyDto dto, string userId, string userRole)
    {

        await _unitOfWork.Begin();
        try
        {
            await _ticketService.ChangeStatusIntoOpen(dto.TicketId, userRole);
            var replyId = await _ticketReplyService.Add(dto, userId, userRole);
            await _unitOfWork.Commit();
            return replyId;
        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }

    }

    public async Task Approve(ApproveReplyDto dto)
    {
        await _unitOfWork.Begin();
        try
        {
            var ticketId = await _ticketReplyService.Approve(dto.ReplyId);
            await _ticketService.ChangeStatusIntoAnswered(ticketId);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.RoleBack();
            throw;
        }

    }
}

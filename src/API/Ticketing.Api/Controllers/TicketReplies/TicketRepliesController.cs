using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts;
using Ticketing.Application.Services.Entities.TicketReplies.Contracts.Dto;
using Ticketing.Application.UseCases.TicketReplies.Contracts;
using Ticketing.Application.UseCases.TicketReplies.Contracts.Dto;
using Ticketing.Shared.JWT;

namespace Ticketing.Api.Controllers.TicketReplies;

[Route("api/ticket-replies")]
[ApiController]
public class TicketRepliesController : ControllerBase
{
    private readonly ITicketReplyHandler _ticketReplyHandler;
    private readonly IUserTokenService _userTokenService;
    private readonly ITicketReplyService _replyService;

    public TicketRepliesController(
        ITicketReplyHandler ticketReplyHandler,
        IUserTokenService userTokenService,
        ITicketReplyService replyService)
    {
        _ticketReplyHandler = ticketReplyHandler;
        _userTokenService = userTokenService;
        _replyService = replyService;
    }


    [Authorize(Roles = SystemRole.Agent + "," + SystemRole.Client)]
    [HttpPost("reply")]

    public async Task<string> ReplyTicketByAgent([FromBody] AddTicketReplyDto dto)
    {

        var userId = _userTokenService.UserId;
        var userRole = _userTokenService.UserRole;

        return await _ticketReplyHandler.Add(dto, userId!, userRole!);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task EditReply(
        [FromRoute] string id,
        [FromBody] EditTicketReplyDto dto)
    {
        await _replyService.Edit(id, dto);
    }

    [Authorize(Roles = SystemRole.Admin + "," +
        SystemRole.Administrator)]
    [HttpPatch("approve-reply")]
    public async Task Approve([FromBody] ApproveReplyDto dto)
    {
        await _ticketReplyHandler.Approve(dto);
    }


    [Authorize(Roles = SystemRole.Client)]
    [HttpPost("reply-by-client")]
    public async Task<string> ReplyTicketByClient([FromBody] ReplyTicketHandlerDto dto)
    {
        throw null;
    }
}

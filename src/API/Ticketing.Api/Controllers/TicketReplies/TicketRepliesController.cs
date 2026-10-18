using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Ticketing.Application;
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


    public TicketRepliesController(
        ITicketReplyHandler ticketReplyHandler,
        IUserTokenService userTokenService)
    {
        _ticketReplyHandler = ticketReplyHandler;
        _userTokenService = userTokenService;
    }


    [Authorize(Roles =SystemRole.Agent)]
    [HttpPost("reply-by-agent")]

    public async Task<string> ReplyTicketByAgent([FromBody] ReplyTicketHandlerDto dto)
    {

        var userId = _userTokenService.UserId;
        return await _ticketReplyHandler.ReplyTicketByAgentAsync(dto,userId!);
    }


    [Authorize(Roles =SystemRole.Client)]
    [HttpPost("reply-by-client")]
    public async Task<string> ReplyTicketByClient([FromBody] ReplyTicketHandlerDto dto)
    {
        var userId = _userTokenService.UserId;
        return await _ticketReplyHandler.ReplyTicketByClient(dto,userId!);

    }
}

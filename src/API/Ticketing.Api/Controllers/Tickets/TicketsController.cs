using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Shared.JWT;

namespace Ticketing.Api.Controllers.Tickets;

[Route("api/tickets")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService  _ticketService;
    private readonly IUserTokenService _userTokenService;


    public TicketsController(
        ITicketService ticketService,
        IUserTokenService userTokenService)
    {
        _ticketService = ticketService;
        _userTokenService = userTokenService;
    }


    [Authorize]
    [HttpPost("add")]
    public async Task<string> Add([FromBody] AddTicketDto dto)
    {

        var userId = _userTokenService.UserId;
        return await _ticketService.Add(dto,userId);
    }
}

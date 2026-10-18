using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Infrastructure.Persistence.Extensions.Paginations;
using Ticketing.Shared.Interfaces.Paginations;
using Ticketing.Shared.JWT;

namespace Ticketing.Api.Controllers.Tickets;

[Route("api/tickets")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
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
        return await _ticketService.Add(dto, userId);
    }

    [Authorize(Roles = SystemRole.Agent)]
    [HttpGet("get-all-for-agent")]
    public async Task<IPageResult<GetAllTicketsDto>> GetAllForAgent(
        [FromQuery] TicketFilterDto? filter = null,
        [FromQuery] Pagination? pagination = null
        )
    {
        return await _ticketService.GetAllForAgent(pagination, filter);
    }

    [Authorize(Roles = SystemRole.Agent)]
    [HttpGet("{id}/for-agent")]
    public async Task<GetTicketDetailsDto?> GetDetailsForAgent([FromRoute] string id)
    {
        return await _ticketService.GetDetailsForAgent(id);
    }

    [Authorize(Roles = SystemRole.Agent)]
    [HttpPatch("change-status-by-agent")]
    public async Task ChangeStatusByAgent([FromBody] ChangeTicketStatusDto dto)
    {
        await _ticketService.ChangeStatusByAgent(dto);
    }


    [Authorize(Roles = SystemRole.Client)]
    [HttpGet("all-for-client")]
    public async Task<IPageResult<GetAllTicketsDto>> GetAllFoClient(
        [FromQuery] TicketFilterDto? filter = null,
        [FromQuery] Pagination? pagination = null
        )
    {

        var userId=_userTokenService.UserId;
        return await _ticketService.GetAllForClient(userId!,filter, pagination);
    }
}

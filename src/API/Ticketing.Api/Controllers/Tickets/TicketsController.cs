using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.Tickets.Contracts;
using Ticketing.Application.Services.Entities.Tickets.Contracts.Dto;
using Ticketing.Application.UseCases.AssignTickets.Contracts;
using Ticketing.Application.UseCases.Tickets.Contracts;
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
    private readonly IAssignHandler _assignHandler;
    private readonly ITicketHandler _ticketHandler;

    public TicketsController(
        ITicketService ticketService,
        IUserTokenService userTokenService,
        IAssignHandler assignHandler,
        ITicketHandler ticketHandler)
    {
        _ticketService = ticketService;
        _userTokenService = userTokenService;
        _assignHandler = assignHandler;
        _ticketHandler = ticketHandler;
    }

    [Authorize(Roles = SystemRole.Client)]
    [HttpPost("add")]
    public async Task<string> Add([FromBody] AddTicketDto dto)
    {

        var userId = _userTokenService.UserId;
        return await _ticketService.Add(dto, userId);
    }


    [Authorize(Roles=SystemRole.Client)]
    [HttpPut("edit")]
    public async Task Edit([FromBody] EditTicketDto dto)
    {
        await _ticketService.Edit(dto);
    }

    [Authorize]
    [HttpDelete("{id}/document/{documentId}/remove-file")]
    public async Task RemoveFile(
        [FromRoute] string documentId,
        [FromRoute]string id)
    {
        var userId=_userTokenService.UserId;
        await _ticketHandler.RemoveDocument(documentId, id, userId!);
    }




    [Authorize]
    [HttpPost("add-file")]
    public async Task AddFile([FromBody] AddTicketFileDto dto)
    {
        await _ticketService.AddFile(dto);
    } 


    [Authorize(Roles =
        SystemRole.Admin + "," +
        SystemRole.Administrator)]
    [HttpGet("get-all-for-admin")]
    public async Task<IPageResult<GetAllTicketsDto>> GetAllForAdmin(
        [FromQuery] TicketFilterDto? filter = null,
        [FromQuery] Pagination? pagination = null,
        [FromQuery] string? search = null
        )
    {
        return await _ticketService.GetAllForAdmin(pagination, filter, search);
    }

    [Authorize(Roles =
        SystemRole.Admin + "," +
        SystemRole.Administrator)]
    [HttpGet("{id}/for-admin")]
    public async Task<GetTicketDetailsDto?> GetDetailsForAdmin([FromRoute] string id)
    {
        return await _ticketService.GetDetailsForAmin(id);
    }


    [Authorize(Roles = SystemRole.Agent)]
    [HttpGet("all-for-agent")]
    public async Task<IPageResult<GetAllAgentTicketsDto>> GetAllAgentTickets(
        [FromQuery] Pagination? pagination = null,
        [FromQuery] TicketFilterDto? filter = null)
    {
        var userId = _userTokenService.UserId;
        return await _ticketService.GetAllAgentTickets(userId!, pagination, filter);
    }

    [Authorize(Roles = SystemRole.Agent)]
    [HttpGet("{id}/details-for-agent")]
    public async Task<GetTicketDetailsForAgentDto?> GetDetailsForAgent([FromRoute] string id)
    {
        return await _ticketService.GetDetailsForAgent(id);
    }

    [Authorize(Roles = SystemRole.Client)]
    [HttpGet("all-for-client")]
    public async Task<IPageResult<GetAllTicketsDto>> GetAllFoClient(
        [FromQuery] TicketFilterDto? filter = null,
        [FromQuery] Pagination? pagination = null
        )
    {
        var userId = _userTokenService.UserId;
        return await _ticketService.GetAllForClient(userId!, filter, pagination);

    }

    [Authorize(Roles = SystemRole.Client)]
    [HttpGet("{id}/get-for-client")]
    public async Task<GetClientTicketDetailsDto?> GetDetailsForClient([FromRoute] string id)
    {
        var userId = _userTokenService.UserId;
        return await _ticketService.GetDetailsForClient(id, userId!);
    }

    [Authorize(Roles = SystemRole.Admin + "," +
        SystemRole.Administrator)]
    [HttpPost("assign-to-agent")]
    public async Task AssignToAgent([FromBody] AssignTicketToAgentDto dto)
    {
        await _assignHandler.AssignToAgent(dto);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.Roles.Contracts;
using Ticketing.Application.Services.Entities.Roles.Contracts.Dto;

namespace Ticketing.Api.Controllers.Roles;
[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [Authorize(Roles = SystemRole.Admin + ","+
        SystemRole.Administrator)]
    [HttpGet("all")]
    public async Task<List<GetAllRolesDto>> GetAll()
    {
        return await _roleService.GetAll();
    }
}

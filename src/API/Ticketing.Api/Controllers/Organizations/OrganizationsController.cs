using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.Organizations.Contracts;
using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;

namespace Ticketing.Api.Controllers.Organizations;
[Route("api/organizations")]
[ApiController]
public class OrganizationsController : ControllerBase
{

    private readonly IOrganizationService _organizationService;

    public OrganizationsController(
        IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }


    [HttpPost]
    [Authorize(Roles = SystemRole.Administrator)]
    public async Task<string> Add([FromBody] AddOrganizationDto dto)
    {
        return await _organizationService.Add(dto);
    }


    [HttpGet("all")]
    [Authorize(Roles = SystemRole.Administrator)]
    public async Task<List<GetAllOrganizationDto>> GetAll([FromQuery] string? search=null)
    {
        return await _organizationService.GetAll(search);
    }
}

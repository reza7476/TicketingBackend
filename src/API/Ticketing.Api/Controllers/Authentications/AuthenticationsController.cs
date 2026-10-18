using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.Services.Authentications.Contracts;
using Ticketing.Application.Services.Authentications.Contracts.Dto;
using Ticketing.Shared.DTOS;

namespace Ticketing.Api.Controllers.Authentications;

[Route("api/authentication")]
[ApiController]
public class AuthenticationsController : ControllerBase
{
    private readonly IAuthenticationAppService _authenticationService;

    public AuthenticationsController(
        IAuthenticationAppService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("log-in")]
    public async Task<GetJwtTokenDto> Login([FromBody] LoginDto dto)
    {
        return await _authenticationService.Login(dto);
    }

    [HttpDelete]
    public async Task Delete()
    {

    }


}

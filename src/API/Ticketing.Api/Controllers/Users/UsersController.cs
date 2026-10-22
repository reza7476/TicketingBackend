using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Application.UseCases.Users.Contracts;
using Ticketing.Application.UseCases.Users.Contracts.Dto;

namespace Ticketing.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        [Authorize(Roles=SystemRole.Administrator +","+
            SystemRole.Admin)]
        public async Task<string> Add([FromBody] AddUserDto dto)
        {
            return await _userService.Add(dto);
        }

    }
}

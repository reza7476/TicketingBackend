using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Infrastructure.Persistence.Extensions.Paginations;
using Ticketing.Shared.Interfaces.Paginations;

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
        [Authorize(Roles = SystemRole.Administrator + "," +
            SystemRole.Admin)]
        public async Task<string> Add([FromBody] AddUserDto dto)
        {
            return await _userService.Add(dto);
        }

        [Authorize(Roles = SystemRole.Administrator)]
        [HttpGet("all-for-administrator")]
        public async Task<IPageResult<GetAllUsersDto>> GetAll(
            [FromQuery] Pagination? pagination = null,
            [FromQuery] UserFilterDto? filter = null,
            [FromQuery] string? search = null
            )
        {
            return await _userService
                .GetAllForAdministrator(pagination, filter, search);
        }

        [HttpGet("{id}/details")]
        [Authorize(Roles =
            SystemRole.Administrator)]
        public async Task<GetUserDetailsDto?> GetDetails([FromRoute] string id)
        {
            return await _userService.GetDetails(id);
        }


        [Authorize(Roles = SystemRole.Administrator)]
        [HttpPut("{id}")]
        public async Task EditUser(
            [FromRoute] string id,
            [FromBody] EditUserDto dto)
        {
            await _userService.Edit(dto, id);
        }
    }
}

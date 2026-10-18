using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.UseCases.Users.Contracts;
using Ticketing.Application.UseCases.Users.Contracts.Dto;

namespace Ticketing.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserHandler _userHandler;

        public UsersController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [HttpPost("sign-up-agent")]
        public async Task SignUp([FromBody] SignUpUserDto dto)
        {
            await _userHandler.SignUpAgent(dto);
        }

        [HttpPost("sign-up-client")]
        public async Task SignUpClient([FromBody] SignUpUserDto dto)
        {
            await _userHandler.SignUpClient(dto);
        }

    }
}

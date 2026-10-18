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
        public async Task SignUp([FromBody]SignUpAgentHandleDto dto)
        {
            await _userHandler.SignUpAgent(dto);
        }

    }
}

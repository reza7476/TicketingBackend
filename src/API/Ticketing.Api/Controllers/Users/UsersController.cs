using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.UseCases.Users.Contracts;

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

        [HttpPost("sign-up")]
        public async Task SignUp(SignUpDto dto)
        {
            await _userHandler.SignUp(dto);
        }

    }
}

using Ticketing.Application.Services.Authentications.Contracts;
using Ticketing.Application.Services.Authentications.Contracts.Dto;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Exceptions;
using Ticketing.Shared.DTOS;
using Ticketing.Shared.Interfaces;
using Ticketing.Shared.JWT;

namespace Ticketing.Application.Services.Authentications;

public class AuthenticationAppService : IAuthenticationAppService
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passHash;
    private readonly IJWTService _jwtService;

    public AuthenticationAppService(
        IUserRepository userRepository,
        IPasswordHasher passHash,
        IJWTService jwtService)
    {
        _userRepository = userRepository;
        _passHash = passHash;
        _jwtService = jwtService;
    }

    public async Task<GetJwtTokenDto> Login(LoginDto dto)
    {
        var userInfo = await _userRepository.FindByMobileForToken(dto.UserName);
        if (userInfo == null)
        {
            throw new UserNotFoundException();
        }
        if (!await _passHash.Verify(userInfo.HashPassword!, dto.Password))
        {
            throw new UserNotFoundException();
        }

        var jwtToken = await _jwtService
            .GenerateToken(new UserInfoForJWTToken()
        {
                Id=userInfo.Id,
                Role=userInfo.RoleTitle
        });

        return jwtToken;    
    }
}

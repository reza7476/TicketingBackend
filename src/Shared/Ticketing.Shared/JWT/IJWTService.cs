using Ticketing.Shared.DTOS;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Shared.JWT;

public interface IJWTService : IService
{
    Task<GetJwtTokenDto> GenerateToken(UserInfoForJWTToken dto);
}

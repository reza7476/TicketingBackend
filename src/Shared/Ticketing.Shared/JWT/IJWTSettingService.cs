
using Ticketing.Shared.DTOS;

namespace Ticketing.Shared.JWT;

public interface IJWTSettingService 
{
    JwtSettingDto JwtSetting { get; }
}

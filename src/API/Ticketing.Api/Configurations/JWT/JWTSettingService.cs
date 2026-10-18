using Ticketing.Shared.DTOS;
using Ticketing.Shared.JWT;

namespace Ticketing.Api.Configurations.JWT;

public class JWTSettingService : IJWTSettingService
{
    public JwtSettingDto JwtSetting { get; private set; }

    public JWTSettingService(IConfiguration configuration)
    {
        JwtSetting = new JwtSettingDto()
        {
            PrivateKey = configuration["Jwt:PrivateKey"]
            ?? throw new InvalidOperationException("Jwt:PrivateKey is not configured"),

            Issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt:Issuer is not configured"),

            Audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt:Audience is not configured"),

            AccessTokenExpirationMinutes = int.Parse(
            configuration["Jwt:AccessTokenExpirationMinutes"]
                ?? throw new InvalidOperationException("Jwt:AccessTokenExpirationMinutes is not configured"))
        };
    }
}

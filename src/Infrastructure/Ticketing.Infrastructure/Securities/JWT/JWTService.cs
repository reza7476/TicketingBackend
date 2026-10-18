using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Ticketing.Application.Services.Authentications.Contracts.Dto;
using Ticketing.Shared.DTOS;
using Ticketing.Shared.JWT;

namespace Ticketing.Infrastructure.Securities.JWT;

public class JWTService : IJWTService
{
    private readonly RsaSecurityKey _privateKey;
    private readonly JwtSettingDto _jwtSettingDto;

    public JWTService(IJWTSettingService jwtSettingService)
    {
        _jwtSettingDto = jwtSettingService.JwtSetting;
        var rsa = RSA.Create();
        rsa.ImportFromPem(_jwtSettingDto.PrivateKey);
        _privateKey = new RsaSecurityKey(rsa);
    }

    public Task<GetJwtTokenDto> GenerateToken(UserInfoForJWTToken dto)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,dto.Id),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, dto.Id),
            new Claim(ClaimTypes.Role, dto.Role)
        };

        
        var credential = new SigningCredentials(_privateKey, SecurityAlgorithms.RsaSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettingDto.Issuer,
            audience: _jwtSettingDto.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettingDto.AccessTokenExpirationMinutes),
            signingCredentials: credential);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(new GetJwtTokenDto
        {
            Token = jwtToken,
        });
    }
}

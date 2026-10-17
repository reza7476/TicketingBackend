using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace LawyerExam.Api.Configurations.JWT;

public static class JWTAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        string publicKeyPem = "";
        publicKeyPem = configuration["Jwt:PublicKey"]!;
        var rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem);
        var key = new RsaSecurityKey(rsa);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "Issuer",

                    ValidateAudience = true,
                    ValidAudience = "Audience",

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key
                };
            });
        return services;
    }
}

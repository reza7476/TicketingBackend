namespace Ticketing.Shared.DTOS;

public class JwtSettingDto
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 30;
    public string PrivateKey { get; set; } = default!;
    public string PublicKey { get; set; } = default!;
}

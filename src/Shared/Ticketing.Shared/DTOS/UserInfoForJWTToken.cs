namespace Ticketing.Shared.DTOS;

public class UserInfoForJWTToken
{
    public string Id { get; set; } = default!;
    public string Role { get; set; } = default!;
}

namespace Ticketing.Application.Services.Entities.Users.Contracts.Dto;

public class GetUserInfoForTokenDto
{
    public string Id { get; set; } = default!;
    public string RoleTitle { get; set; } = default!;
    public string HashPassword { get; set; } = default!;

}

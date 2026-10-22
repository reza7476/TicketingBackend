namespace Ticketing.Application.Services.Entities.Users.Contracts.Dto;

public class AddUserDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string OrganizationId { get; set; } = default!;
}

namespace Ticketing.Application.Services.Entities.Users.Contracts.Dto;

public class GetUserDetailsDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string? Role { get; set; } 
    public string Organization { get; set; } = default!;
}

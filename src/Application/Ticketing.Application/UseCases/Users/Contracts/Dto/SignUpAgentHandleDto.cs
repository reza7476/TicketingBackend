namespace Ticketing.Application.UseCases.Users.Contracts.Dto;

public class SignUpAgentHandleDto
{
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Mobile { get; set; }=default!;
    public string Password { get; set; } = default!;
}

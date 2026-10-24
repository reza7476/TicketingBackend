namespace Ticketing.Application.Services.Entities.Users.Contracts.Dto;

public class GetAllAgentsDto
{
    public string Id { get; set; } = default!;
    public string FirstName { get; set; } =default!;
    public string LastName { get; set; }  =default!;
}

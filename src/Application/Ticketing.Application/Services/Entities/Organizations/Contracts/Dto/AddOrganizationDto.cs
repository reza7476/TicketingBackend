namespace Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
public class AddOrganizationDto
{
    public string Name { get; set; } = default!;
    public bool IsDefault { get; set; } = false;
}

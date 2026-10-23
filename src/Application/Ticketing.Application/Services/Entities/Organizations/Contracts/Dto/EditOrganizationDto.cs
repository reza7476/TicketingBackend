namespace Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;

public class EditOrganizationDto
{
    public string Name { get; set; } = default!;
    public string OrganizationCode { get; set; }=default!;
    public bool IsDefault { get; set; } = false;
}

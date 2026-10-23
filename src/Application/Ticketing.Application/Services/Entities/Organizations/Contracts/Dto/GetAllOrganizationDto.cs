namespace Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
public class GetAllOrganizationDto
{
    public string Name { get; set; } = default!;
    public string Id { get; set; } = default!;
    public bool IsDefault { get; set; }
    public string OrganizationCode { get; set; } = default!;
}

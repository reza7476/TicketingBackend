using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Organizations.Contracts;
public interface IOrganizationService : IService
{
    Task<string> Add(AddOrganizationDto dto);
    Task Edit(EditOrganizationDto dto, string id);
    Task<List<GetAllOrganizationDto>> GetAll(string? search=null);
    Task<GetOrganizationDto?> GetDetails(string id);
}

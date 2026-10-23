using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Organizations.Contracts;
public interface IOrganizationService : IService
{
    Task<string> Add(AddOrganizationDto dto);
    Task<List<GetAllOrganizationDto>> GetAll(string? search=null);
}

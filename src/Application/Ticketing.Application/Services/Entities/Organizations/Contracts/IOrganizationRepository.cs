using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
using Ticketing.Domain.Entities.Organizations;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Organizations.Contracts;
public interface IOrganizationRepository : IRepository
{
    Task Add(Organization organ);
    Task<bool> ExistedById(string id);
    Task<List<GetAllOrganizationDto>> GetAll(string? search=null);
    Task<bool> ExistedByOrganCode(string organCode);
    Task<Organization?> GetById(string id);
    Task<bool> IsExistByOrganCode(string id, string organizationCode);
}

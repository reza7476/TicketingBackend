using Ticketing.Application.Services.Entities.Organizations.Contracts.Dto;
using Ticketing.Domain.Entities.Organizations;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Organizations.Contracts;
public interface IOrganizationRepository : IRepository
{
    Task Add(Organization organ);
    Task<bool> ExistedByName(string name);
    Task<List<GetAllOrganizationDto>> GetAll();
}

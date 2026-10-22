using Ticketing.Application.Services.Entities.Roles.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Roles.Contracts;

public interface IRoleService : IService
{
    Task<List<GetAllRolesDto>> GetAll();
    Task<string> GetRoleIdByRoleName(string roleName);
}

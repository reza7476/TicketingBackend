using Ticketing.Application.Services.Entities.Roles.Contracts.Dto;
using Ticketing.Domain.Entities.Roles;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Roles.Contracts;

public interface IRoleRepository : IRepository
{
    Task Add(Role newRole);
    Task<bool> ExistedById(string id);
    Task<List<GetAllRolesDto>> GetAll();
    Task<Role?> GetById(string roleId);
    Task <string?>GetRoleIdByRoleName(string roleName);
}

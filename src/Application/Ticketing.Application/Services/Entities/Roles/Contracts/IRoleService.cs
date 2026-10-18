using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Roles.Contracts;

public interface IRoleService : IService
{
    Task<string> GetRoleIdByRoleName(string roleName);
}

using Ticketing.Application.Services.Entities.Roles.Contracts;
using Ticketing.Domain.Entities.Roles;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Application.Services.Entities.Roles;

public class RoleAppService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;


    public RoleAppService(
        IRoleRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GetRoleIdByRoleName(string roleName)
    {
        var roleId = await _repository.GetRoleIdByRoleName(roleName);

        if (roleId == null) 
        {
            var newRole = new Role()
            {
                Id = Guid.NewGuid().ToString(),
                Title = roleName,

            };
            await _repository.Add(newRole);

            await _unitOfWork.Complete();
            roleId=newRole.Id;
        }

        return roleId;
    }
}

using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Roles.Contracts;
using Ticketing.Application.Services.Entities.Roles.Contracts.Dto;
using Ticketing.Domain.Entities.Roles;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Roles;

public class RoleAppRepository : IRoleRepository
{
    private readonly DbSet<Role> _roles;


    public RoleAppRepository(AppDbContext context)
    {
        _roles = context.Set<Role>();
    }

    public async Task Add(Role newRole)
    {
        await _roles.AddAsync(newRole);
    }

    public async Task<List<GetAllRolesDto>> GetAll()
    {
        return await _roles.Select(_ => new GetAllRolesDto()
        {
            Id = _.Id,
            Title = _.Title
        }).ToListAsync();
    }

    public async Task<string?> GetRoleIdByRoleName(string roleName)
    {
        return await _roles
            .Where(_ => _.Title == roleName)
            .Select(_ => _.Id)
            .FirstOrDefaultAsync();
    }
}

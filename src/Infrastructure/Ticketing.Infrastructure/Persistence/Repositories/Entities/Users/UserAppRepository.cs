using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Domain.Entities.Roles;
using Ticketing.Domain.Entities.UserRoles;
using Ticketing.Domain.Entities.Users;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Users;

public class UserAppRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DbSet<Role> _roles;
    private readonly DbSet<UserRole> _userRoles;
    public UserAppRepository(AppDbContext context)
    {
        _users = context.Set<User>();
        _roles = context.Set<Role>();
        _userRoles = context.Set<UserRole>();

    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<bool> ExistById(string? userId)
    {
        return await _users.AnyAsync(_ => _.Id == userId);
    }

    public async Task<GetUserInfoForTokenDto?>
        FindByMobileForToken(string mobile)
    {
        var query = await (
                     from user in _users
                     join userRole in _userRoles
                     on user.Id equals userRole.UserId
                     join role in _roles
                     on userRole.RoleId equals role.Id
                     where user.Mobile == mobile
                     select new GetUserInfoForTokenDto()
                     {
                         Id = user.Id,
                         RoleTitle = role.Title,
                         HashPassword = user.HashPassword,
                     }
                   ).FirstOrDefaultAsync();

        return query;
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }
}

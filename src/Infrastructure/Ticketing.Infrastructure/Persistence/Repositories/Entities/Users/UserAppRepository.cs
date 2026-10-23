using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Domain.Entities.Organizations;
using Ticketing.Domain.Entities.Roles;
using Ticketing.Domain.Entities.UserRoles;
using Ticketing.Domain.Entities.Users;
using Ticketing.Infrastructure.Persistence.DatabaseContext;
using Ticketing.Infrastructure.Persistence.Extensions.Paginations;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Users;

public class UserAppRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DbSet<Role> _roles;
    private readonly DbSet<UserRole> _userRoles;
    private readonly DbSet<Organization> _organs;

    public UserAppRepository(AppDbContext context)
    {
        _users = context.Set<User>();
        _roles = context.Set<Role>();
        _userRoles = context.Set<UserRole>();
        _organs = context.Set<Organization>();

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

    public async Task<IPageResult<GetAllUsersDto>>
        GetAllForAdministrator(
         IPagination? pagination = null,
         UserFilterDto? filter = null,
         string? search = null)
    {

        var query = (from user in _users
                     join organ in _organs
                        on user.OrganizationId equals organ.Id
                     join ur in _userRoles
                        on user.Id equals ur.UserId into userRoles
                     from userRole in userRoles.DefaultIfEmpty()
                     join r in _roles
                       on userRole.RoleId equals r.Id into roles
                     from role in roles.DefaultIfEmpty()
                     select new GetAllUsersDto()
                     {
                         CreatedAt = user.CreatedAt,
                         FirstName = user.FirstName,
                         Id = user.Id,
                         LastName = user.LastName,
                         Mobile = user.Mobile,
                         Organization = organ.Name,
                         Role = role != null ? role.Title : null,
                         UserName = user.Mobile
                     }
                   ).AsQueryable();
        if (filter != null)
        {
            if (filter.Organization != null)
            {
                query = query.Where(_ => _.Organization == filter.Organization);
            }

            if (filter.Role != null)
            {
                query = query.Where(_ => _.Role == filter.Role);
            }

        }
        if (search != null)
        {
            search = search.Trim().ToLower();
            query = query.Where(_ =>
            _.FirstName.ToLower() == search ||
            _.LastName.ToLower() == search ||
            _.Mobile.ToLower() == search
            );
        }

        query = query.OrderByDescending(_ => _.CreatedAt);
        return await query.Paginate(pagination ?? new Pagination());

    }

    public async Task<GetUserDetailsDto?> GetDetails(string id)
    {
        return await (from user in _users
                     join organ in _organs
                        on user.OrganizationId equals organ.Id
                     join ur in _userRoles
                        on user.Id equals ur.UserId into userRoles
                        from userRole in userRoles.DefaultIfEmpty()
                     join r in _roles
                       on userRole.RoleId equals r.Id into roles
                       from role in roles
                     where user.Id == id
                     select new GetUserDetailsDto()
                     {
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         Mobile = user.Mobile,
                         Organization = organ.Name,
                         Role = role != null ? role.Title : null
                     }).FirstOrDefaultAsync();
       
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }
}

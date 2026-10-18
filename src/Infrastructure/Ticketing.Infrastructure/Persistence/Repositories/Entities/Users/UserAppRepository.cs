using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Services.Entities.Users.Contracts;
using Ticketing.Domain.Entities.Users;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

namespace Ticketing.Infrastructure.Persistence.Repositories.Entities.Users;

public class UserAppRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserAppRepository(AppDbContext context)
    {
        _users = context.Set<User>();
    }

    public async Task Add(User user)
    {
        await _users.AddAsync(user);
    }

    public async Task<bool> IsExistByMobile(string mobile)
    {
        return await _users.AnyAsync(_ => _.Mobile == mobile);
    }
}

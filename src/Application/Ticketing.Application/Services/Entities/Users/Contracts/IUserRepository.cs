using Ticketing.Domain.Entities.Users;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Users.Contracts;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<bool> ExistById(string? userId);
    Task<bool> IsExistByMobile(string mobile);
}

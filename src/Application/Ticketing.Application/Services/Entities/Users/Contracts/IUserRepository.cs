using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Domain.Entities.Users;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Users.Contracts;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<GetUserInfoForTokenDto?> FindByMobileForToken(string mobile);
    Task<bool> IsExistByMobile(string mobile);
}

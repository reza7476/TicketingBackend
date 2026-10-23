using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Domain.Entities.Users;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Users.Contracts;

public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<bool> ExistById(string? userId);

    Task<GetUserInfoForTokenDto?>
        FindByMobileForToken(string mobile);
    
    Task<IPageResult<GetAllUsersDto>> 
        GetAllForAdministrator(
        IPagination? pagination, 
        UserFilterDto? filter,
        string? search);
    Task<GetUserDetailsDto?> GetDetails(string id);
    Task<bool> IsExistByMobile(string mobile);
}

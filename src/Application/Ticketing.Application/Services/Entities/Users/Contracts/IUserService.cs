using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Application.Services.Entities.Users.Contracts;

public interface IUserService : IService
{
    Task<string> Add(AddUserDto addUserDto);
    Task Edit(EditUserDto dto, string id);
    Task<List<GetAllAgentsDto>> GetAllAgents();
    Task<IPageResult<GetAllUsersDto>>
        GetAllForAdministrator(
        IPagination? pagination=null,
        UserFilterDto? filter = null,
        string? search=null);
    Task<GetUserDetailsDto?> GetDetails(string id);
    Task<GetUserProfileDto?> GetUserProfile(string id);
}

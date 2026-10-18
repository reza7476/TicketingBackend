using Ticketing.Application.Services.Entities.Users.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.Services.Entities.Users.Contracts;

public interface IUserService : IService
{
    Task Add(AddUserDto addUserDto);
}

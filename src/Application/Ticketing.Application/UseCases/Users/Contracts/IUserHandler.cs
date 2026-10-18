using Ticketing.Application.UseCases.Users.Contracts.Dto;
using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.UseCases.Users.Contracts;

public interface IUserHandler : IService
{
    Task SignUpAgent(SignUpAgentHandleDto dto);
}

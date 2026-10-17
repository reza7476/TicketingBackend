using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Application.UseCases.Users.Contracts;

public interface IUserHandler : IService
{
    Task SignUp(SignUpDto dto);
}

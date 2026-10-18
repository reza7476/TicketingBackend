using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Shared.JWT;

public interface IUserTokenService : IScope
{

    string? UserId { get; }
}

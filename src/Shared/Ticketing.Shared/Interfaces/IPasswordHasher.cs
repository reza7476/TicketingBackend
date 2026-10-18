using Ticketing.Shared.Interfaces.LifeTimes;

namespace Ticketing.Shared.Interfaces;

public interface IPasswordHasher : IService
{
    string GeneratePassword(string password);
    Task<bool> Verify(string v, string password);
}

using SHA3.Net;
using System.Text;
using Ticketing.Shared.Interfaces;

namespace Ticketing.Infrastructure.Securities.PasswordHashing;

public class SHAPasswordHasher : IPasswordHasher
{
    public string GeneratePassword(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        var hashBytes = Sha3.Sha3256().ComputeHash(data);
        var hash = Convert.ToHexString(hashBytes);
        return $"{hash}";
    }
}

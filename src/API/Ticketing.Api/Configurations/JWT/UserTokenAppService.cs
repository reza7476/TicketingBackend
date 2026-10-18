using System.Security.Claims;
using Ticketing.Shared.JWT;

namespace Ticketing.Api.Configurations.JWT;

public class UserTokenAppService : IUserTokenService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserTokenAppService(IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor;
    }

    public string? UserId => GetUserIdFromToken();

    private string? GetUserIdFromToken()
    {
        return _contextAccessor.HttpContext?.User.Claims?
            .FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}

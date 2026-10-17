using System.Security.Principal;
using Ticketing.Domain.Entities.UserRoles;

namespace Ticketing.Domain.Entities.Roles;

public class Role
{
    public Role()
    {
        Roles = new List<UserRole>();
    }
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public List<UserRole> Roles { get; set; }
}

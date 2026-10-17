using Ticketing.Domain.Entities.Roles;
using Ticketing.Domain.Entities.Users;

namespace Ticketing.Domain.Entities.UserRoles;

public class UserRole
{
    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;

    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;

}

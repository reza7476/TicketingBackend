using Ticketing.Domain.Entities.Organizations;
using Ticketing.Domain.Entities.UserRoles;

namespace Ticketing.Domain.Entities.Users;

public class User
{
    public User()
    {
        Roles = new List<UserRole>();
    }

    public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string HashPassword { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string OrganizationId { get; set; } = default!;
    public Organization Organization { get; set; } = default!;
    public List<UserRole> Roles { get; set; }

}

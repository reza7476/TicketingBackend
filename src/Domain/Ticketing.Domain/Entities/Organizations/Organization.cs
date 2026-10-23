using System.Reflection.Metadata.Ecma335;
using Ticketing.Domain.Entities.Users;

namespace Ticketing.Domain.Entities.Organizations;
public class Organization
{

    public Organization()
    {
        Users = new List<User>();
    }

    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsDefault { get; set; } = false;

    public List<User> Users { get; set; }
}

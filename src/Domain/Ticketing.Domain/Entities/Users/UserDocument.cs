namespace Ticketing.Domain.Entities.Users;

public class UserDocument
{
    public string Id { get; set; } = default!;
    public string DocumentId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public User User { get; set; } = default!;
}

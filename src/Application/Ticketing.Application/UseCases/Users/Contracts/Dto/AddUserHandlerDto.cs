namespace Ticketing.Application.UseCases.Users.Contracts.Dto;
public  class AddUserHandlerDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public string Password { get; set; } = default!;
}

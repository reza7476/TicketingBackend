namespace Ticketing.Shared.DTOS;

public class ExceptionErrorDto
{
    public string? Error { get; set; }
    public int StatusCode { get; set; }
    public string? Description { get; set; }
    public int RemainingAttempts { get; set; }
    public DateTime? LockUntil { get; set; }
}
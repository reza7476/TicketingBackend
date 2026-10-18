namespace Ticketing.Shared.Interfaces.Paginations;

public interface IPagination
{
    public int? offset { get; set; }
    public int? Limit { get; set; }
}

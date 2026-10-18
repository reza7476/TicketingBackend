using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Infrastructure.Persistence.Extensions.Paginations;

public class PageResult<T> : IPageResult<T> where T : class
{
    public IEnumerable<T> Elements { get; init; }
    public int TotalElements { get; init; }

    public PageResult(IEnumerable<T> elements, int totalElements)
    {
        Elements = elements;
        TotalElements = totalElements;
    }
}

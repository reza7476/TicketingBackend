using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Infrastructure.Persistence.Extensions.Paginations;

public static class QueryExtension
{
    public static async Task<IPageResult<T>> Paginate<T>(
        this IQueryable<T> query,
        IPagination pagination) where T : class
    {

        if (pagination.Limit.HasValue && pagination.offset.HasValue)
            return await query.Page(pagination);
        return await query.Page();

    }
}
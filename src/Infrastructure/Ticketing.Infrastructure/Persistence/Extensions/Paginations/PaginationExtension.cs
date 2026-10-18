using Microsoft.EntityFrameworkCore;
using Ticketing.Shared.Interfaces.Paginations;

namespace Ticketing.Infrastructure.Persistence.Extensions.Paginations;

public static class PaginationExtension
{
    internal static async Task<IPageResult<T>> Page<T>(
        this IQueryable<T> query,
        IPagination pagination) where T : class
    {
        var pageResult = await query
            .Skip(pagination.offset!.Value)
            .Take(pagination.Limit!.Value)
            .ToListAsyncSafe();
        var totalCount = await query.CountAsync();

        return new PageResult<T>(pageResult, totalCount);
    }


    internal static async Task<IPageResult<T>>
        Page<T>(this IQueryable<T> query) where T : class
    {
        var pageResult = await query.ToListAsyncSafe();
        return new PageResult<T>(pageResult, pageResult.Count());
    }


    private static Task<List<TSource>>
        ToListAsyncSafe<TSource>(this IQueryable<TSource> query)
    {
        if (query is null)
            throw new ArgumentNullException(nameof(query));

        if (query is not IAsyncEnumerable<TSource>)
            return Task.FromResult(query.ToList());
        return query.ToListAsync();
    }
}

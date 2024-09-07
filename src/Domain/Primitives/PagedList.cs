using Microsoft.EntityFrameworkCore;
namespace Domain.Primitives;

public class PagedList<T>
{
    public static PagedList<T> Empty => new([], 0, 0, 0);

    public List<T> Items { get; }
    public int Page { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;

    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, Pagination pagination, CancellationToken cancellation)
    {
        var totalCount = await query.CountAsync(cancellation);
        var items = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync(cancellation);
        return new PagedList<T>(items, pagination.PageNumber, pagination.PageSize, totalCount);
    }

    public async Task ForEachAsync(Func<T, Task> asyncAction)
    {
        foreach (var item in Items)
        {
            await asyncAction(item);
        }
    }
    
    public async Task<PagedList<TOut>> MapAsync<TOut>(Func<T, Task<TOut>> asyncMap)
    {
        var items = new List<TOut>();
        foreach (var item in Items)
        {
            var mapped = await asyncMap(item);
            items.Add(mapped);
        }
        return new PagedList<TOut>(items, Page, PageSize, TotalCount);
    }
}

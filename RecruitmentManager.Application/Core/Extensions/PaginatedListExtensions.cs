using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Core.Extensions;

public static class PaginatedListExtensions
{
    public static async Task<PaginatedList<T>> CreateAsync<T>(
        this IQueryable<T> query,
        int page, 
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new(items, page, pageSize, totalCount);
    }
}
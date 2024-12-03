using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Core.Pagination;

internal static class Paginator
{
    public static async Task<PaginationResponse<T>> PaginateAsync<T>(
        IQueryable<T> query,
        PaginationRequest request)
    {
        if (!IsValidSortBy<T>(request.SortBy))
        {
            throw new InvalidSortByException(request.SortBy);
        }

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

        var pagedData = await query
            .OrderByDynamic(request.SortBy, request.SortOrder)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginationResponse<T>
        {
            Data = pagedData,
            TotalCount = totalCount,
            TotalPages = totalPages,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            SortBy = request.SortBy,
            SortOrder = request.SortOrder
        };
    }

    private static bool IsValidSortBy<T>(string sortBy)
    {
        return typeof(T).GetProperties().Any(p => p.Name.Equals(sortBy, StringComparison.CurrentCultureIgnoreCase));
    }
}
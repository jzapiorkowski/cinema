using System.Linq.Expressions;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Infrastructure.Core.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName, SortingOrder sortOrder)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var keySelector = Expression.Lambda(property, parameter);

        var methodName = sortOrder == SortingOrder.Asc ? "OrderBy" : "OrderByDescending";
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type);

        return (IQueryable<T>)method.Invoke(null, new object[] { source, keySelector });
    }
}
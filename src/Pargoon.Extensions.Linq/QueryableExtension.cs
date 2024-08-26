using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pargoon.Extensions.Linq;

public static class QueryableExtension
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition,
        Expression<Func<TSource, bool>> predicate) => condition ? source.Where(predicate) : source;

    public static IQueryable<T> Sorting<T>(this IQueryable<T> query, string? propertyName, SortDirection direction, string? defaultPropertyName = null,
       IComparer<object>? comparer = null)
    {
        propertyName ??= defaultPropertyName;
        if (string.IsNullOrWhiteSpace(propertyName))
            return query;

        return direction == SortDirection.Asc
            ? query.OrderBy(propertyName!, comparer)
            : query.OrderByDescending(propertyName!, comparer);
    }

    public static IQueryable<T> Sorting<T>(this IQueryable<T> query, List<SortItem> sortItems, IComparer<object>? comparer = null)
    {
        if (sortItems == null || sortItems.Count == 0)
            return query;

        var result = query.Sorting(sortItems.First().PropertyName, sortItems.First().Direction, comparer: comparer);
        for (var i = 1; i < sortItems.Count; i++)
        {
            var item = sortItems[i];
            result = item.Direction == SortDirection.Asc
                ? result.ThenBy(item.PropertyName, comparer)
                : result.ThenByDescending(item.PropertyName, comparer);
        }
        return result;
    }

    #region Order IQueryable

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, IComparer<object>? comparer = null)
    {
        return query.CallOrderedQueryable("OrderBy", propertyName, comparer);
    }

    public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object>? comparer = null)
    {
        return query.CallOrderedQueryable("OrderByDescending", propertyName, comparer);
    }

    public static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string propertyName, IComparer<object>? comparer = null)
    {
        return query.CallOrderedQueryable("ThenBy", propertyName, comparer);
    }

    public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object>? comparer = null)
    {
        return query.CallOrderedQueryable("ThenByDescending", propertyName, comparer);
    }

    private static IQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName, string propertyName, IComparer<object>? comparer = null)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

        var methodCall = Expression.Call(
            typeof(Queryable),
            methodName,
            new[] { typeof(T), body.Type },
            query.Expression,
            Expression.Lambda(body, param)
        );

        if (comparer != null)
        {
            methodCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), body.Type },
                query.Expression,
                Expression.Lambda(body, param),
                Expression.Constant(comparer)
            );
        }

        return query.Provider.CreateQuery<T>(methodCall);
    }

    #endregion
}

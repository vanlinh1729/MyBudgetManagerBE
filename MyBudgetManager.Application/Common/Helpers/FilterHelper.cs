using System.Linq.Expressions;

namespace MyBudgetManager.Application.Common.Helpers;

public static class FilterHelper
{
    public static IQueryable<T> ApplyFilter<T>(
        this IQueryable<T> query,
        Expression<Func<T, bool>>? predicate)
    {
        if (predicate != null)
            query = query.Where(predicate);
        return query;
    }
}
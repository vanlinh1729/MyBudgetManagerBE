using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace MyBudgetManager.Application.Common.Helpers;

public static class SortingHelper
{
    public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        string? sortBy,
        string? sortOrder,
        Dictionary<string, Expression<Func<T, object>>>? sortableFields = null)
    {
        if (string.IsNullOrWhiteSpace(sortBy) || sortableFields == null || !sortableFields.ContainsKey(sortBy.ToLower()))
            return query; // default order handled outside if needed

        var keySelector = sortableFields[sortBy.ToLower()];
        bool descending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);

        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }
}
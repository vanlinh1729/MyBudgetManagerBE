using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Common.Constants;

public static class DefaultCategories
{
    public static readonly (string Name, CategoryType Type, string Icon)[] Categories =
    {
        ("Ăn uống", CategoryType.Expense, "utensils"),
        ("Đi lại", CategoryType.Expense, "bus"),
        ("Mua sắm", CategoryType.Expense, "shopping-bag"),
        ("Thu nhập chính", CategoryType.Income, "briefcase"),
        ("Lãi tiết kiệm", CategoryType.Income, "piggy-bank")
    };
}
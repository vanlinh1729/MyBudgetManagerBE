namespace MyBudgetManager.Application.Interfaces.Services;

public interface IDefaultCategoryService
{
    Task CreateDefaultCategoriesForUserAsync(Guid userId);
}
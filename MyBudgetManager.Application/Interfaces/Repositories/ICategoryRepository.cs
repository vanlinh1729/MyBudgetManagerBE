using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface ICategoryRepository: IRepository<Category>
{
    Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId);
}
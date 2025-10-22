using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
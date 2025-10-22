using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface ITransactionRepository: IRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetByUserBalanceIdAsync(Guid userBalanceId);
}

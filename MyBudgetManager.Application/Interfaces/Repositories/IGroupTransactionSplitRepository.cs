using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface IGroupTransactionSplitRepository: IRepository<GroupTransactionSplit>
{
    Task<IEnumerable<GroupTransactionSplit>> GetByTransactionIdAsync(Guid transactionId);
}

using MyBudgetManager.Application.Interfaces.Repositories;

namespace MyBudgetManager.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserBalanceRepository UserBalanceRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    IGroupRepository GroupRepository { get; }
    IGroupMemberRepository GroupMemberRepository { get; }
    ITokenRepository TokenRepository { get; }
    IGroupTransactionSplitRepository GroupTransactionSplitRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
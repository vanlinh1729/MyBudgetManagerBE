using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Repositories;

namespace MyBudgetManager.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository UserRepository { get; }
    public IUserBalanceRepository UserBalanceRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ITransactionRepository TransactionRepository { get; }
    public IGroupRepository GroupRepository { get; }
    public IGroupMemberRepository GroupMemberRepository { get; }
    public ITokenRepository TokenRepository { get; }
    public IGroupTransactionSplitRepository GroupTransactionSplitRepository { get; }


    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository users,
        IUserBalanceRepository userBalances,
        ICategoryRepository categories,
        ITransactionRepository transactions,
        IGroupRepository groups,
        IGroupMemberRepository groupMembers,
        ITokenRepository tokens,
        IGroupTransactionSplitRepository groupTransactionSplits)
    {
        _context = context;
        UserRepository = users;
        UserBalanceRepository = userBalances;
        CategoryRepository = categories;
        TransactionRepository = transactions;
        GroupRepository = groups;
        GroupMemberRepository = groupMembers;
        TokenRepository = tokens;
        GroupTransactionSplitRepository = groupTransactionSplits;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

}
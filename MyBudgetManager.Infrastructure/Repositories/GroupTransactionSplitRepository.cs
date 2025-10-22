using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class GroupTransactionSplitRepository : Repository<GroupTransactionSplit>, IGroupTransactionSplitRepository
{
    public GroupTransactionSplitRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GroupTransactionSplit>> GetByTransactionIdAsync(Guid transactionId)
    {
        return await _context.Set<GroupTransactionSplit>()
            .Where(s => s.TransactionId == transactionId)
            .ToListAsync();
    }
}
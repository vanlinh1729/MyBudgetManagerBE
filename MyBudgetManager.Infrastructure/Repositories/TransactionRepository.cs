using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transaction>> GetByUserBalanceIdAsync(Guid userBalanceId)
    {
        return await _context.Set<Transaction>()
            .Where(t => t.UserBalanceId == userBalanceId)
            .ToListAsync();
    }
}
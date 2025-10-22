using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class UserBalanceRepository : Repository<UserBalance>, IUserBalanceRepository
{
    public UserBalanceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<decimal> GetBalanceByUserIdAsync(Guid userId)
    {
        var ub = await _context.Set<UserBalance>()
            .FirstOrDefaultAsync(x => x.UserId == userId);
        return ub?.Balance ?? 0m;
        
    }
}
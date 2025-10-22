using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class GroupRepository : Repository<Group>, IGroupRepository
{
    public GroupRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Group>> GetGroupsByOwnerAsync(Guid createdById)
    {
        return await _context.Set<Group>()
            .Where(g => g.CreatedBy == createdById)
            .ToListAsync();
    }
}
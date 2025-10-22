using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class GroupMemberRepository : Repository<GroupMember>, IGroupMemberRepository
{
    public GroupMemberRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GroupMember>> GetMembersByGroupIdAsync(Guid groupId)
    {
        return await _context.Set<GroupMember>()
            .Where(m => m.GroupId == groupId)
            .ToListAsync();
    }
}
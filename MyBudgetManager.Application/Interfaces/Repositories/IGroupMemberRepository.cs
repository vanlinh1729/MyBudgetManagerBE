using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface IGroupMemberRepository: IRepository<GroupMember>
{
    Task<IEnumerable<GroupMember>> GetMembersByGroupIdAsync(Guid groupId);
}

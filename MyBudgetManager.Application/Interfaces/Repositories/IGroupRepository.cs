using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface IGroupRepository: IRepository<Group>
{
    Task<IEnumerable<Group>> GetGroupsByOwnerAsync(Guid ownerId);
}

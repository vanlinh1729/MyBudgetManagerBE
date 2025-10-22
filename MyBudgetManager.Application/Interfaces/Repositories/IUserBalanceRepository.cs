using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface IUserBalanceRepository: IRepository<UserBalance>
{
    // thêm hàm đặc thù nếu cần, ví dụ:
    Task<decimal> GetBalanceByUserIdAsync(Guid userId);
}
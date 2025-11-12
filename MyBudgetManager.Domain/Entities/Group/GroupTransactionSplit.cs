using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class GroupTransactionSplit : BaseEntity
{
    public Guid TransactionId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    
    // ✅ Navigation
    public Transaction Transaction { get; set; } = null!;
    public User User { get; set; } = null!;
}
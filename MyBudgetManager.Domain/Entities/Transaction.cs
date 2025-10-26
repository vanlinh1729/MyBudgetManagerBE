using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class Transaction :BaseEntity
{
    public Guid UserBalanceId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? GroupId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Note { get; set; } 
    
    public string? ImageUrl { get; set; }
    
    // ✅ Navigation
    public UserBalance UserBalance { get; set; } = null!;
    public virtual ICollection<GroupTransactionSplit> GroupTransactionSplits { get; set; } = new List<GroupTransactionSplit>();
    public Category Category { get; set; } = null!;
    public Group? Group { get; set; }
}
using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class Category : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public string? Icon { get; set; }

    // ✅ Navigation
    public User User { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
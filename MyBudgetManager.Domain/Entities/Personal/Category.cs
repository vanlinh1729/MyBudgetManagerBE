using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class Category : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public string? Icon { get; set; }
    
    // 🔹 Parent - Child relationship
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    
    public bool IsDefault { get; set; } = false;
    
    // ✅ Navigation
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public User User { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
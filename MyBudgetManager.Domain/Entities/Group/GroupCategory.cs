using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class GroupCategory : BaseEntity
{
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public bool IsDefault { get; set; } = false;
}
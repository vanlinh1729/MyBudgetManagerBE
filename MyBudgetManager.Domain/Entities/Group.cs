using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? GroupAvatarUrl { get; set; }
    public Guid CreatedBy { get; set; }
    
    // ✅ Navigation
    public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
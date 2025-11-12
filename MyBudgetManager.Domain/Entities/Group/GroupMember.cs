using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class GroupMember : BaseEntity  
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public Role Role { get; set; }
    public DateTime JoinedAt { get; set; }
    
    // ✅ Navigation
    public Group Group { get; set; } = null!;
    public User User { get; set; } = null!;
}
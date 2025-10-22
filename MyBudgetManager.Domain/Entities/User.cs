using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Name { get; set; }
    public string? AvatarUrl { get; set; }
    public AccountStatus Status { get; set; }
    public SystemRole SystemRole { get; set; }
    
    //navigation props
    public ICollection<UserBalance> UserBalances { get; set; } = new List<UserBalance>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Token> Tokens { get; set; } = new List<Token>();
    public ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
    public ICollection<GroupTransactionSplit> GroupTransactionSplits { get; set; } = new List<GroupTransactionSplit>();
    
}
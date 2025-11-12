using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class GroupTransaction : BaseEntity
{
    public Guid GroupId { get; set; }
    public Guid PaidByUserId { get; set; }

    public Guid GroupCategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }

    public Group Group { get; set; } = null!;
    public User PaidByUser { get; set; } = null!;
    public GroupCategory GroupCategory { get; set; } = null!;

    public ICollection<GroupTransactionSplit> Splits { get; set; }
        = new List<GroupTransactionSplit>();
}
using System.Runtime.InteropServices;
using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class UserBalance : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public Currency Currency { get; set; }
    public decimal Balance { get; set; }
    
    //nav props
    public User User { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
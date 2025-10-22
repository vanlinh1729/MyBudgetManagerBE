using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    // DbSets
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserBalance> UserBalances { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupMember> GroupMembers { get; set; } = null!;
    public DbSet<Token> Tokens { get; set; } = null!;
    public DbSet<GroupTransactionSplit> GroupTransactionSplits { get; set; } = null!; // nếu có

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

}
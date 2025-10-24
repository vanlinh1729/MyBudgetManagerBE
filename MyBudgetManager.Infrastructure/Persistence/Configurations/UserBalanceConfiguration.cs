using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Persistence.Configurations;

public class UserBalanceConfiguration : IEntityTypeConfiguration<UserBalance>
{
    public void Configure(EntityTypeBuilder<UserBalance> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Currency)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.Balance)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // User → UserBalance: Restrict (cannot delete user if balances exist)
        builder.HasOne(b => b.User)
            .WithMany(u => u.UserBalances)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // UserBalance → Transaction: Cascade (delete balance deletes its transactions)
        builder.HasMany(b => b.Transactions)
            .WithOne(t => t.UserBalance)
            .HasForeignKey(t => t.UserBalanceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
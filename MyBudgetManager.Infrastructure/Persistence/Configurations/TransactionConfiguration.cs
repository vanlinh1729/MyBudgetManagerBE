using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.Note)
            .HasMaxLength(500);

        builder.Property(t => t.Date)
            .IsRequired();
        
        builder.Property(t => t.Type)
            .HasConversion<int>() // Lưu enum dưới dạng số
            .IsRequired();

        // UserBalance → Transaction: Cascade (delete balance deletes its transactions)
        builder.HasOne(t => t.UserBalance)
            .WithMany(b => b.Transactions)
            .HasForeignKey(t => t.UserBalanceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Category → Transaction: Restrict (cannot delete category if transactions exist)
        builder.HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Group → Transaction: SetNull (transactions can exist without group)
        builder.HasOne(t => t.Group)
            .WithMany(g => g.Transactions)
            .HasForeignKey(t => t.GroupId)
            .OnDelete(DeleteBehavior.SetNull);

        // Transaction → GroupTransactionSplit: Cascade
        builder.HasMany(t => t.GroupTransactionSplits)
            .WithOne(s => s.Transaction)
            .HasForeignKey(s => s.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Persistence.Configurations;

public class GroupTransactionSplitConfiguration : IEntityTypeConfiguration<GroupTransactionSplit>
{
    public void Configure(EntityTypeBuilder<GroupTransactionSplit> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Transaction → GroupTransactionSplit: Cascade
        builder.HasOne(s => s.Transaction)
            .WithMany(t => t.GroupTransactionSplits)
            .HasForeignKey(s => s.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        // User → GroupTransactionSplit: Restrict (avoid multiple cascade paths)
        builder.HasOne(s => s.User)
            .WithMany(u => u.GroupTransactionSplits)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Persistence.Configurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.ToTable("Tokens");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TokenValue)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.TokenType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.ExpireAt)
            .IsRequired();

        builder.Property(t => t.RevokedAt)
            .IsRequired(false);

        builder.Property(t => t.ReplacedByToken)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(t => t.DeviceInfo)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(t => t.IpAddress)
            .HasMaxLength(100)
            .IsRequired(false);

        // User → Token: Cascade
        builder.HasOne(t => t.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for performance
        builder.HasIndex(t => t.TokenValue)
            .IsUnique();

        builder.HasIndex(t => t.UserId);
    }
}
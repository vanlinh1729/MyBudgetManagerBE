using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Persistence.Configurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        // 🔑 Table name
        builder.ToTable("Tokens");
       
        // 🆔 Primary key
        builder.HasKey(t => t.Id);

        // 📦 Properties
        builder.Property(t => t.TokenValue)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.TokenType)
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

        // 👤 Relationship
        builder.HasOne(t => t.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ⚡ Index for quick lookup
        builder.HasIndex(t => t.TokenValue)
            .IsUnique();

        builder.HasIndex(t => t.UserId);
        
    }
}
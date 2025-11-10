using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Services.Auth;
using Org.BouncyCastle.Crypto.Generators;

namespace MyBudgetManager.Infrastructure.Persistence.SeedData;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext context, IPasswordHasher hasher)
    {
        // Nếu chưa tạo database thì bỏ qua
        await context.Database.MigrateAsync();

        // ==== SEED ADMIN ====
        var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        if (!await context.Users.AnyAsync(u => u.Id == adminId))
        {
            var admin = new User
            {
                Id = adminId,
                Email = "admin@mbm.com",
                Name = "Administrator",
                PasswordHash = hasher.HashPassword("Admin123!"),
                SystemRole = SystemRole.Admin,
                Status = AccountStatus.Active,
                CreatedAt = DateTime.UtcNow
            };
            context.Users.Add(admin);
        }

        await context.SaveChangesAsync();
    }
}
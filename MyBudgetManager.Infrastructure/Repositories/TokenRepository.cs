using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class TokenRepository : Repository<Token>, ITokenRepository
{
    public TokenRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Token?> GetValidTokenAsync(Guid userId, string tokenValue)
    {
        return await _context.Set<Token>()
            .FirstOrDefaultAsync(t => t.UserId == userId
                                      && t.TokenValue == tokenValue
                                      && t.RevokedAt == null
                                      && t.ExpireAt > DateTime.UtcNow);
    }

    public async Task<Token?> GetByValueAsync(string tokenValue)
    {
        return await _context.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TokenValue == tokenValue);
    }
    
    public async Task<Token?> GetValidRefreshTokenAsync(Guid userId, string tokenValue)
    {
        return await _context.Tokens
            .FirstOrDefaultAsync(t =>
                t.UserId == userId &&
                t.TokenValue == tokenValue &&
                t.TokenType == TokenType.RefreshToken &&
                t.ExpireAt > DateTime.UtcNow &&
                t.RevokedAt == null);
    }

    public async Task RevokeAllUserTokensAsync(Guid userId)
    {
        var tokens = await _context.Tokens
            .Where(t => t.UserId == userId && t.RevokedAt == null)
            .ToListAsync();

        foreach (var token in tokens)
            token.RevokedAt = DateTime.UtcNow;

        _context.Tokens.UpdateRange(tokens);
    }
    
    public async Task<IEnumerable<Token>> GetAllByUserAndTypeAsync(Guid userId, TokenType tokenType)
    {
        return await _context.Tokens
            .Where(t => t.UserId == userId && t.TokenType == tokenType)
            .ToListAsync();
    }
}
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(Guid userId, string email, string role);
    Task<Token> CreateRefreshTokenAsync(Guid userId);
    Task<Token> ValidateRefreshTokenAsync(string refreshToken);
    Task RevokeTokenAsync(Token token);
}
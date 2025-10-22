using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;
using MyBudgetManager.Infrastructure.Settings;

namespace MyBudgetManager.Infrastructure.Services.Auth;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnitOfWork _uow;

    public JwtTokenService(JwtSettings settings, ITokenRepository tokenRepository, IUnitOfWork uow)
    {
        _settings = settings;
        _tokenRepository = tokenRepository;
        _uow = uow;
    }

    public string GenerateAccessToken(Guid userId, string email, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Token> CreateRefreshTokenAsync(Guid userId)
    {
        var token = new Token
        {
            UserId = userId,
            TokenValue = Guid.NewGuid().ToString("N"),
            TokenType = TokenType.RefreshToken,
            ExpireAt = DateTime.UtcNow.AddDays(7)
        };

        await _tokenRepository.AddAsync(token);
        await _uow.SaveChangesAsync();

        return token;
    }

    public async Task<Token> ValidateRefreshTokenAsync(string refreshToken)
    {
        var token = await _tokenRepository.GetByValueAsync(refreshToken)
                    ?? throw new Exception("Invalid refresh token.");

        if (token.ExpireAt < DateTime.UtcNow || token.RevokedAt != null)
            throw new Exception("Expired or revoked token.");

        return token;
    }

    public async Task RevokeTokenAsync(Token token)
    {
        token.RevokedAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync();
    }
}
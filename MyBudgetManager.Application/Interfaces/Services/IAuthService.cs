using MyBudgetManager.Application.Features.Auth.DTOs;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResultDto> LoginAsync(string email, string password);
    Task RegisterAsync(string email, string password, string name);
    Task<LoginResultDto> RefreshTokenAsync(string refreshToken);
    Task RevokeTokenAsync(Guid userId, string refreshToken);

    Task ActivateAccountAsync(string token);
    
    Task ResendActivationEmailAsync(string email);

}
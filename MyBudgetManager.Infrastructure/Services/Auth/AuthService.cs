using MyBudgetManager.Application.Features.Auth.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Services.Auth;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtService;
    private readonly IUnitOfWork _uow;
    private readonly IEmailService _emailService;

    public AuthService(IUserRepository userRepository, ITokenRepository tokenRepository, IPasswordHasher passwordHasher, IJwtTokenService jwtService, IUnitOfWork uow, IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _uow = uow;
        _emailService = emailService;
    }

    public async Task RegisterAsync(string email, string password, string name)
    {
        if (await _userRepository.GetByEmailAsync(email) != null)
            throw new Exception("Email already exists.");

        var hashedPassword = _passwordHasher.HashPassword(password);

        var user = new User
        {
            Email = email,
            PasswordHash = hashedPassword,
            Name = name,
            Status = AccountStatus.Pending,
            SystemRole = SystemRole.User
        };

        await _userRepository.AddAsync(user);
        await _uow.SaveChangesAsync();
        
        var activationToken = Guid.NewGuid().ToString("N");
        var token = new Token
        {
            UserId = user.Id,
            TokenValue = activationToken,
            TokenType = TokenType.ActivationToken,
            ExpireAt = DateTime.UtcNow.AddHours(24)
        };
        await _tokenRepository.AddAsync(token);
        await _uow.SaveChangesAsync();

        // gui mail kich hoat
        await _emailService.SendActivateEmailAsync(user.Email, user.Name, activationToken);
    }

    public async Task<LoginResultDto> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email)
                   ?? throw new Exception("Invalid credentials.");

        if (!_passwordHasher.VerifyPassword( user.PasswordHash, password))
            throw new Exception("Invalid credentials.");

        var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Email, user.SystemRole.ToString());
        var refreshToken = await _jwtService.CreateRefreshTokenAsync(user.Id);

        return new LoginResultDto(accessToken, refreshToken.TokenValue, DateTime.UtcNow.AddMinutes(30));


    }

    public async Task<LoginResultDto> RefreshTokenAsync(string refreshToken)
    {
        var token = await _jwtService.ValidateRefreshTokenAsync(refreshToken);
        var user = await _userRepository.GetByIdAsync(token.UserId)
                   ?? throw new Exception("User not found.");

        await _jwtService.RevokeTokenAsync(token);
        var newToken = await _jwtService.CreateRefreshTokenAsync(user.Id);

        var newAccessToken = _jwtService.GenerateAccessToken(user.Id, user.Email, user.SystemRole.ToString());

        return new LoginResultDto(newAccessToken, newToken.TokenValue, DateTime.UtcNow.AddMinutes(30));
    }

    public async Task RevokeTokenAsync(Guid userId, string refreshToken)
    {
        var token = await _tokenRepository.GetValidTokenAsync(userId, refreshToken)
                    ?? throw new Exception("Token not found.");
        await _jwtService.RevokeTokenAsync(token);
    }
    public async Task ActivateAccountAsync(string tokenValue)
    {
        var token = await _tokenRepository.GetByValueAsync(tokenValue);

        if (token == null || token.TokenType != TokenType.ActivationToken)
            throw new Exception("Invalid activation token");

        if (token.ExpireAt < DateTime.UtcNow)
            throw new Exception("Activation token expired");

        var user = await _userRepository.GetByIdAsync(token.UserId);
        if (user == null)
            throw new Exception("User not found");

        user.Status = AccountStatus.Active;
        _tokenRepository.Remove(token);
        _userRepository.Update(user);

        await _uow.SaveChangesAsync();
    }

    public async Task ResendActivationEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) throw new Exception("User not found");
        if (user.Status == AccountStatus.Active) throw new Exception("User already active");

        // Xóa token cũ
        var oldTokens = await _tokenRepository.GetAllByUserAndTypeAsync(user.Id, TokenType.ActivationToken);
        foreach (var t in oldTokens) _tokenRepository.Remove(t);
        await _uow.SaveChangesAsync();

        var newTokenValue = Guid.NewGuid().ToString("N");
        var newToken = new Token
        {
            UserId = user.Id,
            TokenValue = newTokenValue,
            TokenType = TokenType.ActivationToken,
            ExpireAt = DateTime.UtcNow.AddHours(24)
        };

        await _tokenRepository.AddAsync(newToken);
        await _uow.SaveChangesAsync();

        // 🔹 4. Gửi lại email
        await _emailService.SendActivateEmailAsync(user.Email, user.Name, newTokenValue);

    }
}
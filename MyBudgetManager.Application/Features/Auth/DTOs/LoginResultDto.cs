namespace MyBudgetManager.Application.Features.Auth.DTOs;

public record LoginResultDto(string AccessToken, string RefreshToken, DateTime ExpireAt);

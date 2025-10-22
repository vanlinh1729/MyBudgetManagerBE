using MediatR;
using MyBudgetManager.Application.Features.Auth.DTOs;

namespace MyBudgetManager.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResultDto>;

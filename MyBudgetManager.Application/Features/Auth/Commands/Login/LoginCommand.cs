using MediatR;
using MyBudgetManager.Application.Features.Auth.DTOs;

namespace MyBudgetManager.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<LoginResultDto>;

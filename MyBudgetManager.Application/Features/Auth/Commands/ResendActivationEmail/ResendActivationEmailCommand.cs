using MediatR;

namespace MyBudgetManager.Application.Features.Auth.Commands.ResendActivationEmail;

public record ResendActivationEmailCommand(string Email) : IRequest<Unit>;

using MediatR;

namespace MyBudgetManager.Application.Features.Auth.Commands.ActivateAccount;

public record ActivateAccountCommand(string Token) : IRequest<Unit>;

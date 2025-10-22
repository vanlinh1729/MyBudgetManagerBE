using MediatR;

namespace MyBudgetManager.Application.Features.Auth.Commands.RevokeToken;

public record RevokeTokenCommand(Guid UserId, string RefreshToken) : IRequest<Unit>;

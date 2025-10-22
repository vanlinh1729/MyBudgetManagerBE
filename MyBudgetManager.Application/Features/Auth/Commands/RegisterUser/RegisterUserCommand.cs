using MediatR;

namespace MyBudgetManager.Application.Features.Auth.Commands.RegisterUser;

public record RegisterUserCommand(
    string Email,
    string Password,
    string Name
) : IRequest<Unit>; 
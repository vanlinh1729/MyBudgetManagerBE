using MediatR;
using MyBudgetManager.Application.Interfaces.Services;

namespace MyBudgetManager.Application.Features.Auth.Commands.ActivateAccount;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Unit>
{
    private readonly IAuthService _authService;

    public ActivateAccountCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<Unit> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        await _authService.ActivateAccountAsync(request.Token);

        return Unit.Value;
    }
}
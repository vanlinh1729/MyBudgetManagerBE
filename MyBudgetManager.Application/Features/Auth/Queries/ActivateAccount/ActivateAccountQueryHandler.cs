using MediatR;
using MyBudgetManager.Application.Interfaces.Services;

namespace MyBudgetManager.Application.Features.Auth.Queries.ActivateAccount;

public class ActivateAccountQueryHandler : IRequestHandler<ActivateAccountQuery, Unit>
{
    private readonly IAuthService _authService;

    public ActivateAccountQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<Unit> Handle(ActivateAccountQuery request, CancellationToken cancellationToken)
    {
        await _authService.ActivateAccountAsync(request.Token);

        return Unit.Value;
    }
}
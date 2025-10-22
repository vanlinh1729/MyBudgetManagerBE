using MediatR;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Features.Auth.Commands.ResendActivationEmail;

public class ResendActivationEmailCommandHandler : IRequestHandler<ResendActivationEmailCommand, Unit>
{
    private readonly IAuthService _authService;

    public ResendActivationEmailCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Unit> Handle(ResendActivationEmailCommand request, CancellationToken cancellationToken)
    {
       await _authService.ResendActivationEmailAsync(request.Email);

        return Unit.Value;
    }
}
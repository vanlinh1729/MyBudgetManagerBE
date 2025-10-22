using MediatR;
using MyBudgetManager.Application.Features.Auth.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommand, LoginResultDto>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.Email, request.Password);
    }
}
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManager.Application.Features.Auth.Commands.Login;
using MyBudgetManager.Application.Features.Auth.Commands.RefreshToken;
using MyBudgetManager.Application.Features.Auth.Commands.RegisterUser;
using MyBudgetManager.Application.Features.Auth.Commands.ResendActivationEmail;
using MyBudgetManager.Application.Features.Auth.Commands.RevokeToken;
using MyBudgetManager.Application.Features.Auth.Queries.ActivateAccount;

namespace MyBudgetManager.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Đăng ký người dùng mới
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Registration successful" });
    }

    /// <summary>
    /// Đăng nhập
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Refresh Token
    /// </summary>
    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Revoke Token
    /// </summary>
    [HttpPost("revoke")]
    public async Task<IActionResult> RevokeToken(RevokeTokenCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Token revoked successfully" });
    }
    
    /// <summary>
    /// Activate tài khoản 
    /// </summary>
    [HttpGet("activate")]
    public async Task<IActionResult> Activate([FromQuery] string token)
    {
        await _mediator.Send(new ActivateAccountQuery(token));
        return Ok(new { message = "Account activated successfully!" });
    }

    /// <summary>
    /// Gửi lại mail active tài khoản
    /// </summary>
    [HttpPost("resend-activation")]
    public async Task<IActionResult> ResendActivationEmail([FromBody] ResendActivationEmailCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "Activation email resent successfully." });
    }

}
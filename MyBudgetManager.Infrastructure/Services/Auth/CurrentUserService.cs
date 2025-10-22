using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyBudgetManager.Application.Interfaces.Services;

namespace MyBudgetManager.Infrastructure.Services.Auth;

public class CurrentUserService : ICurrentUserService
{
    public Guid? UserId { get; }
    public string? Email { get; }
    public string? Role { get; }

    public bool IsAuthenticated => UserId.HasValue;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated == true)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(id, out var guid))
                UserId = guid;

            Email = user.FindFirstValue(ClaimTypes.Email);
            Role = user.FindFirstValue(ClaimTypes.Role);
        }
        else
        {
            UserId = null;
            Email = null;
            Role = null;
        }
    }
}
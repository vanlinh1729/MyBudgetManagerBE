using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Features.Users.DTOs;

public class UserDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string? AvatarUrl { get; set; }
    public AccountStatus Status { get; set; }
    public SystemRole SystemRole { get; set; }
}
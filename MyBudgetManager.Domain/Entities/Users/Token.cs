using System.Text.Json;
using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Domain.Entities;

public class Token : BaseEntity
{
    public Guid UserId { get; set; }
    public string TokenValue { get; set; }
    public TokenType TokenType { get; set; }
    public DateTime ExpireAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    
    public string? ReplacedByToken { get; set; }   // Token mới thay thế token này
    public string? DeviceInfo { get; set; }         // Thiết bị hoặc platform
    public string? IpAddress { get; set; }          // IP người đăng nhập

    
    // ✅ Navigation
    public User User { get; set; } = null!;
    
    public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpireAt;

}
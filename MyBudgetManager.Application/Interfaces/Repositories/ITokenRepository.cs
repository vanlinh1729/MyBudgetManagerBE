using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface ITokenRepository: IRepository<Token>
{
    Task<Token?> GetValidTokenAsync(Guid userId, string tokenValue);
    Task<Token?> GetByValueAsync(string tokenValue);

    
    /// Lấy token hợp lệ (chưa hết hạn, chưa bị thu hồi).
    Task<Token?> GetValidRefreshTokenAsync(Guid userId, string tokenValue);

    /// Thu hồi toàn bộ refresh token của user (nếu cần logout toàn hệ thống).
    Task RevokeAllUserTokensAsync(Guid userId);
    
    Task<IEnumerable<Token>> GetAllByUserAndTypeAsync(Guid userId, TokenType tokenType);
    Task DeleteAllByUserAndTypeAsync(Guid userId, TokenType tokenType);


}
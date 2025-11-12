using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Features.UserBalances.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Services;

namespace MyBudgetManager.Application.Features.UserBalances.Queries.GetUserBalance;


public class GetUserBalanceQueryHandler : IRequestHandler<GetUserBalanceQuery, UserBalanceSummaryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public GetUserBalanceQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<UserBalanceSummaryDto> Handle(GetUserBalanceQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("User not authenticated.");

        var balance = await _unitOfWork.UserBalanceRepository
            .GetQuery()
            .FirstOrDefaultAsync(b => b.UserId == userId, cancellationToken);

        if (balance == null)
            throw new InvalidOperationException("User balance not found. Please contact support.");

        return new UserBalanceSummaryDto
        {
            Balance = balance.Balance,
            Currency = balance.Currency.ToString()
        };
    }
}
using MediatR;
using MyBudgetManager.Application.Features.UserBalances.DTOs;

namespace MyBudgetManager.Application.Features.UserBalances.Queries.GetUserBalance;

public record GetUserBalanceQuery : IRequest<UserBalanceSummaryDto>
{
    
}
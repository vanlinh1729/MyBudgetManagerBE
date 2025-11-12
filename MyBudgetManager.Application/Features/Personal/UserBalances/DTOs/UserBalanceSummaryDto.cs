using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Features.UserBalances.DTOs;

public class UserBalanceSummaryDto
{
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    
}
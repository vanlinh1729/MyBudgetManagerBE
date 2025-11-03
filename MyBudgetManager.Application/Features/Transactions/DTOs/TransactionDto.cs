using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Features.Transactions.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }
    public Guid UserBalanceId { get; set; }
    public Guid CategoryId { get; set; }
    public TransactionType Type { get; set; }
    public Guid? GroupId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
    public string? ImageUrl { get; set; }
}
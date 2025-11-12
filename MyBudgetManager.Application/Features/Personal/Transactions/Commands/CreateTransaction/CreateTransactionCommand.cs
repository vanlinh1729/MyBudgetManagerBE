using MediatR;
using MyBudgetManager.Application.Features.Transactions.DTOs;

namespace MyBudgetManager.Application.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommand : IRequest<TransactionDto>
{
    public Guid UserBalanceId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }
}
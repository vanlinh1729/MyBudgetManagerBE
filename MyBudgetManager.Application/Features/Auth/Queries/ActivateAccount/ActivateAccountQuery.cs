using MediatR;

namespace MyBudgetManager.Application.Features.Auth.Queries.ActivateAccount;

public record ActivateAccountQuery(string Token) : IRequest<Unit>;

using FluentValidation;

namespace MyBudgetManager.Application.Features.Auth.Queries.ActivateAccount;

public class ActivateAccountQueryValidator : AbstractValidator<ActivateAccountQuery>
{
    public ActivateAccountQueryValidator()
    {
        RuleFor(x=>x.Token).NotEmpty().WithMessage("ActivationToken is required.");
    }
}
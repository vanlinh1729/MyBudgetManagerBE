using FluentValidation;

namespace MyBudgetManager.Application.Features.Auth.Commands.ResendActivationEmail;

public class ResendActivationEmailCommandValidator : AbstractValidator<ResendActivationEmailCommand>
{
    public ResendActivationEmailCommandValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");
    }
}
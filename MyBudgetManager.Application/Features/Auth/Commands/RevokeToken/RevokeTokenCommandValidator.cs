using FluentValidation;

namespace MyBudgetManager.Application.Features.Auth.Commands.RevokeToken;

public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(x=> x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("RefreshToken is required.");
    }
}
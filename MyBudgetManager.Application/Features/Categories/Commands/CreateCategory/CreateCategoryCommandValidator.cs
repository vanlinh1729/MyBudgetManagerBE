using FluentValidation;

namespace MyBudgetManager.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name of Category is required")
            .MaximumLength(100);
        RuleFor(x => x.Type).IsInEnum();
    }
}
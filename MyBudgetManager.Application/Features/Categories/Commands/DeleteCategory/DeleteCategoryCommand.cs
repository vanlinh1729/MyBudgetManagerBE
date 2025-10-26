using MediatR;

namespace MyBudgetManager.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>
{
    
}
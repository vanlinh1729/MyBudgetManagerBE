using MediatR;
using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name, CategoryType Type, string? Icon, Guid? ParentCategoryId) 
    : IRequest<Unit>;

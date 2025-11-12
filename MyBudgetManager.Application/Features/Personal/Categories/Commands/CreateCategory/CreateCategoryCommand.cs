using MediatR;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    Guid? UserId,
    string Name,
    CategoryType Type,
    string? Icon,
    Guid? ParentCategoryId
) : IRequest<CategoryDto>; 
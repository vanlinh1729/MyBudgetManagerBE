using MediatR;
using MyBudgetManager.Application.Features.Categories.DTOs;

namespace MyBudgetManager.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>{}
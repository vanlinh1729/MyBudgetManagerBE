using MediatR;
using MyBudgetManager.Application.Features.Categories.DTOs;

namespace MyBudgetManager.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<List<CategoryDto>>
{
}
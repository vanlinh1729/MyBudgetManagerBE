using MediatR;
using MyBudgetManager.Application.Common.Models;
using MyBudgetManager.Application.Features.Categories.DTOs;

namespace MyBudgetManager.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQuery : PagedRequest, IRequest<PagedResult<CategoryDto>>
{
    public string? Type { get; set; } // Optional: "Income" or "Expense"

}
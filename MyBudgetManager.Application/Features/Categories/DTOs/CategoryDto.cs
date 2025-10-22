using MyBudgetManager.Domain.Common;

namespace MyBudgetManager.Application.Features.Categories.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public CategoryType Type { get; set; }
}
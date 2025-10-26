namespace MyBudgetManager.Application.Common.Models;

public class SortRequest
{
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; } = "desc";
}
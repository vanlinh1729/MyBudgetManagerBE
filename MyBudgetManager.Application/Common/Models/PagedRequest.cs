namespace MyBudgetManager.Application.Common.Models;

public class PagedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "CreatedAt"; // default sort
    public string? SortOrder { get; set; } = "desc";   // asc or desc
}
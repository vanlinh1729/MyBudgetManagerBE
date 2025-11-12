using System.Linq.Expressions;
using MediatR;
using MyBudgetManager.Application.Common.Helpers;
using MyBudgetManager.Application.Common.Models;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Common;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<PagedResult<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("User not authenticated.");

        // 🔹 1. Base query
        var query = _unitOfWork.CategoryRepository
            .GetQuery()
            .Where(c => c.UserId == userId);

        // 🔹 2. Apply filter bằng FilterHelper
        if (!string.IsNullOrEmpty(request.Type) &&
            Enum.TryParse<CategoryType>(request.Type, true, out var categoryType))
            query = query.ApplyFilter(c => c.Type == categoryType);

        // 🔹 3. Định nghĩa map field có thể sort
        var sortableFields = new Dictionary<string, Expression<Func<Category, object>>>
        {
            ["name"] = c => c.Name,
            ["createdat"] = c => c.CreatedAt
        };

        // 🔹 4. Apply sort bằng SortingHelper
        query = query.ApplySorting(request.SortBy, request.SortOrder, sortableFields);

        // 🔹 5. Nếu không có sortBy → sort mặc định
        if (string.IsNullOrWhiteSpace(request.SortBy))
            query = query.OrderByDescending(c => c.CreatedAt);

        // 🔹 6. Select sang DTO và apply pagination helper
        var pagedResult = await query
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                Icon = c.Icon,
                IsDefault = c.IsDefault
            })
            .ToPagedResultAsync(request.PageNumber, request.PageSize, cancellationToken);

        return pagedResult;
    }
}
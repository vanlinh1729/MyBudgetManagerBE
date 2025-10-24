using MediatR;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("User not authenticated.");

        var categories = await _unitOfWork
            .CategoryRepository              // lấy repository cho entity Category
            .GetQuery()                          // lấy IQueryable<Category>
            .Where(c => c.UserId == userId)      // filter theo User hiện tại
            .Select(c => new CategoryDto         // map sang DTO
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                Icon = c.Icon
            })
            .ToListAsync(cancellationToken);

        return categories;
    }
}
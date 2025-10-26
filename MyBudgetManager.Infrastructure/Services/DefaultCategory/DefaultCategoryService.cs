using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Common.Constants;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Infrastructure.Services.DefaultCategory;

public class DefaultCategoryService : IDefaultCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public DefaultCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateDefaultCategoriesForUserAsync(Guid userId)
    {
        var existing = await _unitOfWork.CategoryRepository
            .GetQuery()
            .Where(c => c.UserId == userId && c.IsDefault)
            .AnyAsync();

        if (existing) return; // tránh tạo trùng

        var defaults = DefaultCategories.Categories.Select(c => new Category
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = c.Name,
            Type = c.Type,
            Icon = c.Icon,
            IsDefault = true,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _unitOfWork.CategoryRepository.AddRangeAsync(defaults);
        await _unitOfWork.SaveChangesAsync();
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Application.Interfaces;

namespace MyBudgetManager.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetQuery()
                           .Where(c => c.Id == request.Id)
                           .Select(c => new CategoryDto
                           {
                               Id = c.Id,
                               Name = c.Name,
                               Type = c.Type,
                               Icon = c.Icon,
                               IsDefault = c.IsDefault,
                               ParentCategoryId = c.ParentCategoryId
                           })
                           .FirstOrDefaultAsync(cancellationToken)
                       ?? throw new KeyNotFoundException("Category not found.");

        return category;
    }
}
using MediatR;
using MyBudgetManager.Application.Interfaces;

namespace MyBudgetManager.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id)
                       ?? throw new KeyNotFoundException("Category not found.");

        if (category.IsDefault)
            throw new InvalidOperationException("Cannot modify or delete default category.");
        category.Name = request.Name;
        category.Type = request.Type;
        category.Icon = request.Icon;
        category.ParentCategoryId = request.ParentCategoryId;

        _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
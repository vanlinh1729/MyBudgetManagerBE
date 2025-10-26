using MediatR;
using MyBudgetManager.Application.Interfaces;

namespace MyBudgetManager.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id)
                       ?? throw new KeyNotFoundException("Category not found.");

        if (category.IsDefault)
            throw new InvalidOperationException("Cannot modify or delete default category.");
        _unitOfWork.CategoryRepository.Remove(category);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
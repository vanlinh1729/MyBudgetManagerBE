using AutoMapper;
using MediatR;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            UserId = request.UserId,
            Name = request.Name,
            Type = request.Type,
            Icon = request.Icon
        };

        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        // Map sang DTO để trả ra ngoài
        return _mapper.Map<CategoryDto>(category);
    }
}
using AutoMapper;
using MediatR;
using MyBudgetManager.Application.Common.Exceptions;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Application.Interfaces;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Application.Interfaces.Services;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;


    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedException("User not authenticated");

        var category = new Category
        {
            UserId = userId,
            Name = request.Name,
            Type = request.Type,
            Icon = request.Icon,
            ParentCategoryId = request.ParentCategoryId,
            IsDefault = false
        };
        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        // Map sang DTO để trả ra ngoài
           return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            Icon = category.Icon,
            ParentCategoryId = category.ParentCategoryId,
        };
    }
}
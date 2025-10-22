using AutoMapper;
using MyBudgetManager.Application.Features.Categories.DTOs;
using MyBudgetManager.Application.Features.Transactions.DTOs;
using MyBudgetManager.Domain.Entities;

namespace MyBudgetManager.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Transaction, TransactionDto>();
    }
}
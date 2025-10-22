using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MyBudgetManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // Nếu có AutoMapper:
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        return services;
    }
}
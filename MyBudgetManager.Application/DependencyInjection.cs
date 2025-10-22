using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyBudgetManager.Application.Common.Behaviours;

namespace MyBudgetManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // ✅ Đăng ký tất cả các Handler (Command + Query)
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // ✅ Đăng ký tất cả Validator trong Assembly Application
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // ✅ Add pipeline behavior cho validation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Nếu có AutoMapper:
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        return services;
    }
}
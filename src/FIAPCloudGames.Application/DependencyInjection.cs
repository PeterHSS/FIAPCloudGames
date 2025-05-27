using System.Reflection;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;
using FIAPCloudGames.Application.Validators.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAPCloudGames.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddValidators();

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        #region Users

        services.AddScoped<CreateUserUseCase>();

        services.AddScoped<LoginUseCase>();

        services.AddScoped<GetUserByIdUseCase>();

        services.AddScoped<GetAllUsersUseCase>();

        services.AddScoped<UpdateUserUseCase>();

        #endregion

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped, includeInternalTypes: true);

        return services;
    }
}

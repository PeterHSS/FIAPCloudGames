using System.Reflection;
using FIAPCloudGames.Application.UseCases.Games;
using FIAPCloudGames.Application.UseCases.Promotions;
using FIAPCloudGames.Application.UseCases.Users;
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

        #region Promotions

        services.AddScoped<CreatePromotionUseCase>();

        services.AddScoped<UpdatePromotionUseCase>();

        #endregion

        #region Games

        services.AddScoped<CreateGameUseCase>();

        services.AddScoped<UpdateGameUseCase>();

        #endregion

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped, includeInternalTypes: true);

        return services;
    }
}

using FIAPCloudGames.Application.UseCases.Users;
using FIAPCloudGames.Application.Validators.Abstractions;
using FIAPCloudGames.Application.Validators.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAPCloudGames.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region Use Cases

        services.AddScoped<CreateUserUseCase>();

        #endregion

        #region Validators

        services.AddValidatorsFromAssembly(typeof(BaseValidator<>).Assembly, includeInternalTypes: true);

        services.AddScoped<ICreateUserValidator, CreateUserValidator>();

        #endregion

        return services;
    }
}

using FIAPCloudGames.Api.Middlewares;

namespace FIAPCloudGames.Api.Extensions;

public static class GlobalExceptionHandlerExtensions
{
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
        
        return services;
    }
}

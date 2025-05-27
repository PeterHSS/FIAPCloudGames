using Asp.Versioning;
using FIAPCloudGames.Api.Extensions;
using System.Reflection;

namespace FIAPCloudGames.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGenWithAuth();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.AddJwtAuthenticationAndAuthorization(configuration);    

        return services;
    }
}

using FIAPCloudGames.Domain.Enums;
using FIAPCloudGames.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAPCloudGames.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FIAPCloudGamesDbContext>(options =>options.UseSqlServer(configuration.GetConnectionString(DatabaseEnum.FIAPCloudGames.ToString())));

        return services;
    }
}

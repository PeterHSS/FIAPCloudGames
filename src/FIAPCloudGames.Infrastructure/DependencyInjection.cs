using FIAPCloudGames.Application.Abstractions.Infrastructure;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Enums;
using FIAPCloudGames.Infrastructure.Commom;
using FIAPCloudGames.Infrastructure.Persistence;
using FIAPCloudGames.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAPCloudGames.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FIAPCloudGamesDbContext>(options =>options.UseSqlServer(configuration.GetConnectionString(DatabaseEnum.FIAPCloudGames.ToString())));

        #region Repositories

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        #endregion

        #region Commons

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        #endregion

        return services;
    }
}

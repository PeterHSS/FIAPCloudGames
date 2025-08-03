using FIAPCloudGames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        FIAPCloudGamesDbContext dbContext = scope.ServiceProvider.GetRequiredService<FIAPCloudGamesDbContext>();

        dbContext.Database.Migrate();
    }
}

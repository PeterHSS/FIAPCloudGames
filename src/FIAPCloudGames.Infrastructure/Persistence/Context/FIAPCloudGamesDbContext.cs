using FIAPCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infrastructure.Persistence.Context;

public sealed class FIAPCloudGamesDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    public FIAPCloudGamesDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FIAPCloudGamesDbContext).Assembly);
    }
}

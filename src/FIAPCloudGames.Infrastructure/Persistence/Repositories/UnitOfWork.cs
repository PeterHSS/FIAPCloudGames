using System.Data;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly FIAPCloudGamesDbContext _context;

    public UnitOfWork(FIAPCloudGamesDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IDbTransaction BeginTransaction(CancellationToken cancellationToken = default)
    {
        IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction();

        return dbContextTransaction.GetDbTransaction();
    }
}

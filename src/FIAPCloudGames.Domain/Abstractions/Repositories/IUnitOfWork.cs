using System.Data;

namespace FIAPCloudGames.Domain.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction(CancellationToken cancellationToken = default);
}

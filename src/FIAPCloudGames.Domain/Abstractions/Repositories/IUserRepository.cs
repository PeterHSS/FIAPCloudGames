using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllWithGamesAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdWithGamesync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUniqueDocument(string document, CancellationToken cancellationToken = default);
}

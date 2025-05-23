using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;

    public UserRepository(IGenericRepository<User> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _genericRepository.AddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetAllAsync(cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return  await _genericRepository.GetFirstOrDefaultAsyncWithFilter(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _genericRepository.UpdateAsync(user, cancellationToken);
    }
}

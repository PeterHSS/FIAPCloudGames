using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;
    private readonly FIAPCloudGamesDbContext _context;

    public UserRepository(IGenericRepository<User> genericRepository, FIAPCloudGamesDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _genericRepository.AddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetAllAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllWithGamesAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Games)
            .IgnoreQueryFilters()
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetFirstOrDefaultAsyncWithFilter (x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<User?> GetByIdWithGamesync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Games)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueDocument(string document, CancellationToken cancellationToken = default)
    {
        return !await _context.Users
            .AsNoTracking()
            .AnyAsync(user => user.Document == document, cancellationToken);
    }

    public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken = default)
    {
        return !await _context.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email == email, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _genericRepository.UpdateAsync(user, cancellationToken);
    }
}

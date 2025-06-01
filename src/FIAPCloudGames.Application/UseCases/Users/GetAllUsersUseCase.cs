using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        Log.Information("Retrieving all users...");

        IEnumerable<User> users = await _userRepository.GetAllWithGamesAsync(cancellationToken);

        Log.Information("Retrieved {Count} users.", users.Count());

        return users.Select(UserResponse.Create);
    }
}

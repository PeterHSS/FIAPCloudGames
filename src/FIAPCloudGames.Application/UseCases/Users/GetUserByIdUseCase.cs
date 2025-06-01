using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class GetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Log.Information("Retrieving user with ID {UserId}", id);

        User? user = await _userRepository.GetByIdWithGamesync(id, cancellationToken);

        if (user is null)
        {
            Log.Warning("User with ID {UserId} not found", id);

            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        Log.Information("User with ID {UserId} retrieved successfully.", id);

        return UserResponse.Create(user);
    }
}

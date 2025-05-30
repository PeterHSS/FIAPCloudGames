using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

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
        User? user = await _userRepository.GetByIdWithGamesync(id, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        return UserResponse.Create(user);
    }
}

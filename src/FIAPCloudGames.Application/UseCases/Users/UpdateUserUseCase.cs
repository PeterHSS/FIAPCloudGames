using FIAPCloudGames.Application.Abstractions.Infrastructure.Services;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserUseCase(
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task HandleAsync(Guid id, UpdateUserRequest request)
    {
        if (id != _currentUserService.UserId)
            throw new UnauthorizedAccessException("You are not allowed to update this user.");

        User? user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        user.Name = request.Name;

        user.Nickname = request.Nickname;

        await _userRepository.UpdateAsync(user);
    }
}

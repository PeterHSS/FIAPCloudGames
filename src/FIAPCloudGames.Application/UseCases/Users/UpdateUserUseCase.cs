using FIAPCloudGames.Application.Abstractions.Infrastructure.Services;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUserRepository userRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        Log.Information("Start updating user. {@UserId} {@Request}", id, request);

        if (id != _currentUserService.UserId)
        {
            Log.Warning("Unauthorized update attempt. {@CurrentUserId} tried to update {@TargetUserId}", _currentUserService.UserId, id);

            throw new UnauthorizedAccessException("You are not allowed to update this user.");
        }

        User? user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            Log.Warning("User not found for update. {@UserId}", id);

            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        user.UpdateInformation(request.Name, request.Nickname);

        _userRepository.Update(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("User updated successfully. {@UserId} {@UpdatedFields}", id, new { request.Name, request.Nickname });
    }
}

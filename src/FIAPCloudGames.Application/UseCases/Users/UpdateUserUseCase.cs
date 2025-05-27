using FIAPCloudGames.Application.Abstractions.Infrastructure.Services;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FluentValidation;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateUserRequest> _validator;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserUseCase(
        IUserRepository userRepository,
        IValidator<UpdateUserRequest> validator,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _validator = validator;
        _currentUserService = currentUserService;
    }

    public async Task HandleAsync(Guid id, UpdateUserRequest request)
    {
        if (id != _currentUserService.UserId)
            throw new UnauthorizedAccessException("You are not allowed to update this user.");

        await _validator.ValidateAndThrowAsync(request);

        User? user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        user.Name = request.Name;
        
        user.Nickname = request.Nickname;

        await _userRepository.UpdateAsync(user);
    }
}

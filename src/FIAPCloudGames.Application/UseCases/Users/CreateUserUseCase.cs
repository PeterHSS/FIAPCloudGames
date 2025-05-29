using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.Helpers;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherProvider _passwordHasher;

    public CreateUserUseCase(
        IUserRepository userRepository,
        IPasswordHasherProvider passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(CreateUserRequest request)
    {
        string hashedPassword = _passwordHasher.Hash(request.Password);

        User user = User.Create(request.Name, request.Email, hashedPassword, request.Nickname, request.Document.OnlyNumbers(), request.BirthDate);

        await _userRepository.AddAsync(user);
    }
}

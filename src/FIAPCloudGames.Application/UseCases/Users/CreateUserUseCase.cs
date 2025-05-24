using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FluentValidation;

namespace FIAPCloudGames.Application.UseCases.Users;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateUserRequest> _validator;
    private readonly IPasswordHasherProvider _passwordHasher;

    public CreateUserUseCase(IUserRepository userRepository, IValidator<CreateUserRequest> validator, IPasswordHasherProvider passwordHasher)
    {
        _userRepository = userRepository;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(CreateUserRequest request)
    {
        await _validator.ValidateAndThrowAsync(request);

        string hashedPassword = _passwordHasher.Hash(request.Password);

        User user = User.Create(request.Name, request.Email, hashedPassword, request.Nickname, request.Document, request.BirthDate);

        await _userRepository.AddAsync(user);
    }
}

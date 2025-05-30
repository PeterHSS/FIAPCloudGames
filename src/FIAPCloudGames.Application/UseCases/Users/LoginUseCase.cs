using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FluentValidation;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherProvider _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasherProvider passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<LoginResponse> HandleAsync(LoginRequest request, CancellationToken cancellation = default)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email, cancellation);

        if (user is null)
            throw new KeyNotFoundException("Invalid email or password.");

        bool verified = _passwordHasher.Verify(request.Password, user.Password);

        if (!verified)
            throw new KeyNotFoundException("Invalid email or password.");

        string token = _jwtProvider.Create(user);

        return new LoginResponse(token);
    }
}

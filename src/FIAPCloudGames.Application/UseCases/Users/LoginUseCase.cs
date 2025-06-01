using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

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
        Log.Information("Attempting to log in user with email: {Email}", request.Email);

        User? user = await _userRepository.GetByEmailAsync(request.Email, cancellation);

        if (user is null)
        {
            Log.Warning("Login failed for email: {Email} - User not found", request.Email);
         
            throw new KeyNotFoundException("Invalid email or password.");
        }

        bool verified = _passwordHasher.Verify(request.Password, user.Password);

        if (!verified)
        {
            Log.Warning("Login failed for email: {Email} - Invalid password", request.Email);
         
            throw new KeyNotFoundException("Invalid email or password.");
        }

        string token = _jwtProvider.Create(user);
        
        Log.Information("User {Email} logged in successfully", request.Email);

        return new LoginResponse(token);
    }
}

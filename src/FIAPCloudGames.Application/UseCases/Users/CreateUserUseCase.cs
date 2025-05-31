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
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserUseCase(IUserRepository userRepository, IPasswordHasherProvider passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        if (!await _userRepository.IsUniqueEmail(request.Email.ToLower(), cancellationToken))
            throw new ArgumentException("Email already exists.");

        if (!await _userRepository.IsUniqueDocument(request.Document.OnlyNumbers(), cancellationToken))
            throw new ArgumentException("Document already exists.");

        string hashedPassword = _passwordHasher.Hash(request.Password);

        User user = User.Create(request.Name, request.Email, hashedPassword, request.Nickname, request.Document.OnlyNumbers(), request.BirthDate);

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

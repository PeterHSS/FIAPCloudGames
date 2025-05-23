using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.Validators.Abstractions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FluentValidation;

namespace FIAPCloudGames.Application.Validators.Users;

internal sealed class CreateUserValidator : BaseValidator<CreateUserRequest>, ICreateUserValidator   
{
    private readonly IUserRepository _userRepository;

    public CreateUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name can have a maximum of 200 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(254).WithMessage("Email can have a maximum of 254 characters.")
            .EmailAddress().WithMessage("Email format is invalid.")
            .MustAsync(BeUniqueEmail).WithMessage("Email is already in use.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Must(BeAValidPassword).WithMessage("Password must contain letters, numbers, and special characters.");

        RuleFor(user => user.Document)
            .NotEmpty().WithMessage("Document is required.")
            .MaximumLength(14).WithMessage("Document can have a maximum of 14 characters.");

        RuleFor(user => user.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .Must(BeInThePast).WithMessage("BirthDate must be in the past.");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        return user is null;
    }

    private static bool BeInThePast(DateTime dateTime) 
        => dateTime < DateTime.UtcNow;

    private static bool BeAValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        return password.Any(char.IsLetter) &&
               password.Any(char.IsDigit) &&
               password.Any(c => !char.IsLetterOrDigit(c));
    }
}

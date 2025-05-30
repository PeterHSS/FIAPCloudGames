using FIAPCloudGames.Application.DTOs.Games;
using FluentValidation;

namespace FIAPCloudGames.Application.Validators.Game;

public sealed class CreateGameValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameValidator()
    {
        RuleFor(game => game.Name)
            .NotEmpty().WithMessage("The name is required.")
            .MaximumLength(500).WithMessage("The name must be at most 500 characters long.");

        RuleFor(game => game.Description)
            .MaximumLength(1000).WithMessage("The description must be at most 1000 characters long.");

        RuleFor(game => game.ReleasedAt)
            .NotEmpty().WithMessage("The release date is required.");

        RuleFor(game => game.Price)
            .NotNull().WithMessage("The price is required.")
            .GreaterThanOrEqualTo(0).WithMessage("The price must be greater than or equal to zero.");

        RuleFor(game => game.Genre)
            .NotEmpty().WithMessage("The genre is required.")
            .MaximumLength(50).WithMessage("The genre must be at most 50 characters long.");
    }
}

using FIAPCloudGames.Application.DTOs.Promotion;
using FluentValidation;

namespace FIAPCloudGames.Application.Validators.Promotion;

public sealed class CreatePromotionValidator : AbstractValidator<CreatePromotionRequest>
{
    public CreatePromotionValidator()
    {
        RuleFor(request => request.Name)
            .MaximumLength(500).WithMessage("Name must not be empty and must not exceed 500 characters.");

        RuleFor(request => request.StartDate)
            .Must(date => date != default).WithMessage("Start date must not be default value.");

        RuleFor(request => request.EndDate)
            .NotEmpty().WithMessage("End date must not be empty.")
            .GreaterThan(request => request.StartDate).WithMessage("End date must be after the start date.");

        RuleFor(request => request.DiscountPercentage)
            .NotEmpty().WithMessage("Discount percentage must not be empty.")
            .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");

        RuleFor(request => request.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
    }
}

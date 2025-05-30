using System.Linq.Expressions;
using FluentValidation;

namespace FIAPCloudGames.Application.Validators.Promotion;

public abstract class AbstractPromotionValidator<T> : AbstractValidator<T>
{
    protected void AddNameRule(Expression<Func<T, string>> name)
    {
        RuleFor(name)
            .MaximumLength(500).WithMessage("Name must not be empty and must not exceed 500 characters.");
    }

    protected void AddStartDateRule(Expression<Func<T, DateTime>> startDate)
    {
        RuleFor(startDate)
            .Must(date => date != default).WithMessage("Start date must not be default value.");
    }

    protected void AddEndDateRule(Expression<Func<T, DateTime>> endDate, Expression<Func<T, DateTime>> startDate)
    {
        RuleFor(endDate)
            .NotEmpty().WithMessage("End date must not be empty.")
            .GreaterThan(startDate).WithMessage("End date must be after the start date.");
    }

    protected void AddDiscountPercentageRule(Expression<Func<T, decimal>> discountPercentage)
    {
        RuleFor(discountPercentage)
            .NotEmpty().WithMessage("Discount percentage must not be empty.")
            .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");
    }

    protected void AddDescriptionRule(Expression<Func<T, string>> description)
    {
        RuleFor(description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
    }
}

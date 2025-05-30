using FIAPCloudGames.Application.DTOs.Promotions;

namespace FIAPCloudGames.Application.Validators.Promotion;

public sealed class UpdatePromotionValidator : AbstractPromotionValidator<UpdatePromotionRequest>
{
    public UpdatePromotionValidator()
    {
        AddNameRule(request => request.Name);

        AddStartDateRule(request => request.StartDate);

        AddEndDateRule(request => request.EndDate, request => request.StartDate);

        AddDiscountPercentageRule(request => request.DiscountPercentage);

        AddDescriptionRule(request => request.Description);
    }
}

using FIAPCloudGames.Application.DTOs.Games;

namespace FIAPCloudGames.Application.DTOs.Promotions;

public record PromotionResponse(Guid Id, string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description, IEnumerable<GameResponse> Games)
{
    public static PromotionResponse Create(FIAPCloudGames.Domain.Entities.Promotion promotion)
    {
        return new(promotion.Id, promotion.Name, promotion.StartDate, promotion.EndDate, promotion.DiscountPercentage, promotion.Description, promotion.Games.Select(GameResponse.Create));
    }
}
namespace FIAPCloudGames.Application.DTOs.Promotions;

public record UpdatePromotionRequest(string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description);

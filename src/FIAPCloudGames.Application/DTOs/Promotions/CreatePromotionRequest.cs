namespace FIAPCloudGames.Application.DTOs.Promotion;

public record CreatePromotionRequest(string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description);
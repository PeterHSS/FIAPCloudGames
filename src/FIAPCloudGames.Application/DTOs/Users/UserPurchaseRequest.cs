namespace FIAPCloudGames.Application.DTOs.Users;

public record UserPurchaseRequest(IEnumerable<Guid> gamesIds);


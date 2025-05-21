using FIAPCloudGames.Domain.Entities.Base;

namespace FIAPCloudGames.Domain.Entities;

public class Promotion : Entity
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal DiscountPercentage { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}

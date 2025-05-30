using FIAPCloudGames.Domain.Entities.Base;

namespace FIAPCloudGames.Domain.Entities;

public class Promotion : Entity
{
    private Promotion() { }

    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public string Description { get; private set; } 
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<Game> Games { get; init; }

    public static Promotion Create(string name, DateTime startDate, DateTime endDate, decimal discountPercentage, string description)
    {
        return new Promotion
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            DiscountPercentage = discountPercentage,
            Description = description,
            Games = []
        };
    }

    public void Update(string name, DateTime startDate, DateTime endDate, decimal discountPercentage, string description)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        DiscountPercentage = discountPercentage;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}

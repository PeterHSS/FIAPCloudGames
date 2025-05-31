using FIAPCloudGames.Domain.Entities.Base;

namespace FIAPCloudGames.Domain.Entities;

public class Game : Entity
{
    private Game() {  }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime ReleasedAt { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public string Genre { get; private set; } = string.Empty;
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<User> Users { get; private set; } = [];

    public static Game Create(string name, string description, DateTime releasedAt, decimal price, string genre)
    {
        return new Game
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = name,
            Description = description,
            ReleasedAt = releasedAt,
            Price = price,
            IsActive = true,
            Genre = genre
        };
    }

    public void Delete()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description, DateTime releasedAt, decimal price, string genre)
    {
        Name = name;
        Description = description;
        ReleasedAt = releasedAt;
        Price = price;
        Genre = genre;
        UpdatedAt = DateTime.UtcNow;
    }
}

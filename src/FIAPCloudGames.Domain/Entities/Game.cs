using FIAPCloudGames.Domain.Entities.Base;

namespace FIAPCloudGames.Domain.Entities;

public class Game : Entity
{
    private Game() { }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ReleasedAt { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public string Genre { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; } 
    public ICollection<User> Users { get; set; }
}

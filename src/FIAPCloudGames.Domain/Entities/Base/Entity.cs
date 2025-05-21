namespace FIAPCloudGames.Domain.Entities.Base;
public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

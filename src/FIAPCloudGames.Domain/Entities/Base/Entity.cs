namespace FIAPCloudGames.Domain.Entities.Base;

public abstract class Entity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
}

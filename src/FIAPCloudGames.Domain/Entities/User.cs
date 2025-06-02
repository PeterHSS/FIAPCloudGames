using FIAPCloudGames.Domain.Entities.Base;
using FIAPCloudGames.Domain.Enums;

namespace FIAPCloudGames.Domain.Entities;

public class User : Entity
{
    private User() { }

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public string Nickname { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty;    
    public DateTime BirthDate { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public RoleEnum Role { get; private set; }
    public ICollection<Game> Games { get; set; } = [];

    public static User Create(string name, string email, string password, string nickname, string document, DateTime birthDate)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = name,
            Email = email.ToLower(),
            Password = password,
            Nickname = nickname,
            Document = document,
            BirthDate = birthDate,
            Role = RoleEnum.User,
        };
    }

    public bool HasPurchasedGame(Game game)
    {
        return Games.Any(g => g.Id == game.Id);
    }

    public void PurchaseGame(Game game)
    {
        if (HasPurchasedGame(game))
            return;

        Games.Add(game);
    }

    public void UpdateInformation(string name, string nickname)
    {
        Name = name;
        Nickname = nickname;
        UpdatedAt = DateTime.UtcNow;
    }
}

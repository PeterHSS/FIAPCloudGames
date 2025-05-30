using FIAPCloudGames.Domain.Entities.Base;
using FIAPCloudGames.Domain.Enums;

namespace FIAPCloudGames.Domain.Entities;

public class User : Entity
{
    private User() { }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Nickname { get; private set; }
    public string Document { get; private set; }
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

    public void UpdateInformation(string name, string nickname)
    {
        Name = name;
        Nickname = nickname;
        UpdatedAt = DateTime.UtcNow;
    }
}

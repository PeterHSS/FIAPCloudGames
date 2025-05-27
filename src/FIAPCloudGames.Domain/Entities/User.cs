using FIAPCloudGames.Domain.Entities.Base;
using FIAPCloudGames.Domain.Enums;

namespace FIAPCloudGames.Domain.Entities;

public class User : Entity
{
    private User() { }

    public string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Nickname { get; set; }
    public required string Document { get; set; }
    public required DateTime BirthDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public RoleEnum Role { get; set; }
    public ICollection<Game> Games { get; set; } = [];

    public static User Create(string name, string email, string password, string nickname, string document, DateTime birthDate)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = name,
            Email = email,
            Password = password,
            Nickname = nickname,
            Document = document,
            BirthDate = birthDate,
            Role = RoleEnum.User,
        };
    }
}

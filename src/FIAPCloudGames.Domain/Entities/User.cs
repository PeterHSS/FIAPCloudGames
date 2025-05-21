using FIAPCloudGames.Domain.Entities.Base;

namespace FIAPCloudGames.Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public required string Email { get; set; } 
    public required string Password { get; set; } 
    public string Nickname { get; set; }   
    public required string Document { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime UpdatedAt { get; set; }
}

namespace FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;

public interface IPasswordHasherProvider
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}

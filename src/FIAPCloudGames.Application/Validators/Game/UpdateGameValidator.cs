using FIAPCloudGames.Application.DTOs.Games;

namespace FIAPCloudGames.Application.Validators.Game;

public sealed class UpdateGameValidator : AbstractGameValidator<UpdateGameRequest>
{
    public UpdateGameValidator()
    {
        AddNameRule(request => request.Name);
     
        AddDescriptionRule(request => request.Description);
        
        AddReleasedAtRule(request => request.ReleasedAt);
        
        AddPriceRule(request => request.Price);
        
        AddGenreRule(request => request.Genre);
    }
}

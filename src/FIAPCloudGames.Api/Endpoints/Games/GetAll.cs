
using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Application.UseCases.Games;

namespace FIAPCloudGames.Api.Endpoints.Games;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("games",
            async (GetAllGamesUseCase useCase, CancellationToken cancellationToken) =>
            {
                IEnumerable<GameResponse> response = await useCase.HandleAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Games);
            //.RequireAuthorization();
    }
}

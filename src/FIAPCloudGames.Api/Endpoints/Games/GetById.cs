using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Application.UseCases.Games;

namespace FIAPCloudGames.Api.Endpoints.Games;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("games/{id:guid}",
            async (Guid id, GetGameByIdUseCase useCase, CancellationToken cancellationToken) =>
            {
                GameResponse response = await useCase.HandleAsync(id, cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Games)
            .RequireAuthorization();
    }
}

using FIAPCloudGames.Api.Authorization;
using FIAPCloudGames.Application.UseCases.Games;

namespace FIAPCloudGames.Api.Endpoints.Games;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("games/{id:guid}",
            async (Guid id, DeleteGameUseCase useCase, CancellationToken cancellationToken) =>
            {
                await useCase.HandleAsync(id, cancellationToken);

                return Results.NoContent();
            })
            .WithTags(Tags.Games)
            .RequireAuthorization(AuthorizationPolicies.AdministratorOnly);
    }
}

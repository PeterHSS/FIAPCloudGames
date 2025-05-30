using FIAPCloudGames.Api.Authorization;
using FIAPCloudGames.Application.UseCases.Promotions;

namespace FIAPCloudGames.Api.Endpoints.Promotions;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("promotions/{id:guid}",
            async (Guid id, DeletePromotionUseCase useCase, CancellationToken cancellation) =>
            {
                await useCase.HandleAsync(id, cancellation);

                return Results.NoContent();
            })
            .WithTags(Tags.Promotions)
            .RequireAuthorization(AuthorizationPolicies.AdministratorOnly);
    }
}

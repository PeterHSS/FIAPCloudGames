using FIAPCloudGames.Api.Authorization;
using FIAPCloudGames.Application.UseCases.Promotions;

namespace FIAPCloudGames.Api.Endpoints.Promotions;

public sealed class AddGame : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("promotions/{promotionId:guid}/games/{gameId:guid}",
            async (Guid promotionId, Guid gameId, AddGameToPromotionUseCase useCase, CancellationToken cancellationToken) =>
            {
                await useCase.HandleAsync(promotionId, gameId, cancellationToken);
                
                return Results.Accepted();
            })
            .WithTags(Tags.Promotions) 
            .RequireAuthorization(AuthorizationPolicies.AdministratorOnly);
    }
}

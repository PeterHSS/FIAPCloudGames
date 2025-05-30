using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Application.UseCases.Promotions;

namespace FIAPCloudGames.Api.Endpoints.Promotions;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("promotions/{id:guid}",
            async (Guid id, GetPromotionByIdUseCase useCase, CancellationToken cancellationToken) =>
            {
                PromotionResponse response = await useCase.HandleAsync(id, cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Promotions)
            .RequireAuthorization();
    }
}

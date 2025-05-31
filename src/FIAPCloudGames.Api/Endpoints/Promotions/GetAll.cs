using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Application.UseCases.Promotions;

namespace FIAPCloudGames.Api.Endpoints.Promotions;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("promotions",
            async (GetAllPromotionsUseCase useCase, CancellationToken cancellationToken) =>
            {
                IEnumerable<PromotionResponse> response = await useCase.HandleAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Promotions)
            .RequireAuthorization();
    }
}

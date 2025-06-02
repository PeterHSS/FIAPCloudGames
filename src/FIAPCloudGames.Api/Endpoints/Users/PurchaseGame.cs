using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

public sealed class PurchaseGame : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/purchases",
            async (UserPurchaseRequest request, PurchaseGamesUseCase useCase, CancellationToken cancellationToken) =>
            {
                await useCase.HandleAsync(request, cancellationToken);

                return Results.NoContent();
            })
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}

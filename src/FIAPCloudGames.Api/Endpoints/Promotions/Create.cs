using FIAPCloudGames.Api.Authorization;
using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Application.UseCases.Promotions;
using FluentValidation;

namespace FIAPCloudGames.Api.Endpoints.Promotions;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("promotions",
            async (CreatePromotionRequest request, CreatePromotionUseCase useCase, IValidator<CreatePromotionRequest> validator, CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(request, cancellationToken);

                return Results.Created();
            })
            .WithTags(Tags.Promotions)
            .RequireAuthorization(AuthorizationPolicies.AdministratorOnly);
    }
}

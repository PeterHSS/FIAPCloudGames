using FIAPCloudGames.Api.Authorization;
using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Application.UseCases.Games;
using FluentValidation;

namespace FIAPCloudGames.Api.Endpoints.Games;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/games/{id:guid}",
            async (Guid id, UpdateGameRequest request, UpdateGameUseCase useCase, IValidator<UpdateGameRequest> validator, CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(id, request, cancellationToken);

                return Results.NoContent();
            })
            .WithTags(Tags.Games)
            .RequireAuthorization(AuthorizationPolicies.AdministratorOnly);
    }
}

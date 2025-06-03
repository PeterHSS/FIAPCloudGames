using FIAPCloudGames.Api.Authorization;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users",
            async (GetAllUsersUseCase useCase, CancellationToken cancellationToken) =>
            {
                IEnumerable<UserResponse> response = await useCase.HandleAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Users)
            .RequireAuthorization(AuthorizationPolicies.AdministratorOnly);
    }
}

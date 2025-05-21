
namespace FIAPCloudGames.Api.Endpoints.Users;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id:guid}", async (Guid id) =>
        {
            var result = string.Empty;

            await Task.Delay(1000);

            return Results.Ok(result);
        })
        .WithTags(Tags.Users); 
    }
}

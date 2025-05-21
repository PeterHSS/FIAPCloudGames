namespace FIAPCloudGames.Api.Endpoints.Users;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}", async (Guid id) =>
        {
            await Task.Delay(100);

            return Results.Ok(new { string.Empty });
        })
        .WithTags(Tags.Users);
    }
}

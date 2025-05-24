namespace FIAPCloudGames.Api.Endpoints.Users;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async () =>
        {
            string result = string.Empty;

            await Task.Delay(1000);

            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}

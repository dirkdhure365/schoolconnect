namespace SchoolConnect.Institution.Api.Endpoints;

public static class ResourceEndpoints
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/resources")
            .WithTags("Resources");

        // Placeholder endpoints - to be implemented
        group.MapGet("", () => Results.Ok(new { message = "Resource endpoints to be implemented" }))
            .WithName("GetResources")
            .WithOpenApi();
    }
}

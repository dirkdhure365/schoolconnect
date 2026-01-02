namespace SchoolConnect.Institution.Api.Endpoints;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/teams")
            .WithTags("Teams");

        // Placeholder endpoints - to be implemented
        group.MapGet("", () => Results.Ok(new { message = "Team endpoints to be implemented" }))
            .WithName("GetTeams")
            .WithOpenApi();
    }
}

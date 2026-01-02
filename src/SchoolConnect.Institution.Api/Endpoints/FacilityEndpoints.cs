namespace SchoolConnect.Institution.Api.Endpoints;

public static class FacilityEndpoints
{
    public static void MapFacilityEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/facilities")
            .WithTags("Facilities");

        // Placeholder endpoints - to be implemented
        group.MapGet("", () => Results.Ok(new { message = "Facility endpoints to be implemented" }))
            .WithName("GetFacilities")
            .WithOpenApi();
    }
}

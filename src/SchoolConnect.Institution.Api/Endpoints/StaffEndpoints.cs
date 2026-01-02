namespace SchoolConnect.Institution.Api.Endpoints;

public static class StaffEndpoints
{
    public static void MapStaffEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/staff")
            .WithTags("Staff");

        // Placeholder endpoints - to be implemented
        group.MapGet("", () => Results.Ok(new { message = "Staff endpoints to be implemented" }))
            .WithName("GetStaff")
            .WithOpenApi();
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Institution.Application.Commands.Institutes;
using SchoolConnect.Institution.Application.Queries.Institutes;

namespace SchoolConnect.Institution.Api.Endpoints;

public static class InstituteEndpoints
{
    public static void MapInstituteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/institutes")
            .WithTags("Institutes");

        // GET /api/institutes - List institutes
        group.MapGet("", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetInstitutesQuery());
            return Results.Ok(result);
        })
        .WithName("GetInstitutes")
        .WithOpenApi();

        // POST /api/institutes - Create institute
        group.MapPost("", async ([FromBody] CreateInstituteCommand command, [FromServices] IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/institutes/{id}", new { id });
        })
        .WithName("CreateInstitute")
        .WithOpenApi();

        // GET /api/institutes/{id} - Get institute by ID
        group.MapGet("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetInstituteByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetInstituteById")
        .WithOpenApi();

        // PUT /api/institutes/{id} - Update institute
        group.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateInstituteCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateInstitute")
        .WithOpenApi();

        // DELETE /api/institutes/{id} - Deactivate institute
        group.MapDelete("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new DeactivateInstituteCommand { Id = id });
            return Results.NoContent();
        })
        .WithName("DeactivateInstitute")
        .WithOpenApi();

        // GET /api/institutes/{id}/settings - Get institute settings
        group.MapGet("{id:guid}/settings", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetInstituteSettingsQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetInstituteSettings")
        .WithOpenApi();

        // PUT /api/institutes/{id}/settings - Update institute settings
        group.MapPut("{id:guid}/settings", async (Guid id, [FromBody] UpdateInstituteSettingsCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateInstituteSettings")
        .WithOpenApi();

        // POST /api/institutes/{id}/logo - Upload institute logo
        group.MapPost("{id:guid}/logo", async (Guid id, [FromBody] UploadInstituteLogoCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UploadInstituteLogo")
        .WithOpenApi();

        // GET /api/institutes/{id}/dashboard - Get institute dashboard
        group.MapGet("{id:guid}/dashboard", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetInstituteDashboardQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetInstituteDashboard")
        .WithOpenApi();
    }
}

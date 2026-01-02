using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Institution.Application.Commands.Centres;
using SchoolConnect.Institution.Application.Queries.Centres;

namespace SchoolConnect.Institution.Api.Endpoints;

public static class CentreEndpoints
{
    public static void MapCentreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api")
            .WithTags("Centres");

        // GET /api/institutes/{instituteId}/centres - List centres by institute
        group.MapGet("institutes/{instituteId:guid}/centres", async (Guid instituteId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCentresByInstituteQuery { InstituteId = instituteId });
            return Results.Ok(result);
        })
        .WithName("GetCentresByInstitute")
        .WithOpenApi();

        // POST /api/institutes/{instituteId}/centres - Create centre
        group.MapPost("institutes/{instituteId:guid}/centres", async (Guid instituteId, [FromBody] CreateCentreCommand command, [FromServices] IMediator mediator) =>
        {
            if (instituteId != command.InstituteId)
                return Results.BadRequest("Institute ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/centres/{id}", new { id });
        })
        .WithName("CreateCentre")
        .WithOpenApi();

        var centreGroup = app.MapGroup("/api/centres")
            .WithTags("Centres");

        // GET /api/centres/{id} - Get centre by ID
        centreGroup.MapGet("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCentreByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetCentreById")
        .WithOpenApi();

        // PUT /api/centres/{id} - Update centre
        centreGroup.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateCentreCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateCentre")
        .WithOpenApi();

        // DELETE /api/centres/{id} - Deactivate centre
        centreGroup.MapDelete("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new DeactivateCentreCommand { Id = id });
            return Results.NoContent();
        })
        .WithName("DeactivateCentre")
        .WithOpenApi();

        // GET /api/centres/{id}/dashboard - Get centre dashboard
        centreGroup.MapGet("{id:guid}/dashboard", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCentreDashboardQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetCentreDashboard")
        .WithOpenApi();
    }
}

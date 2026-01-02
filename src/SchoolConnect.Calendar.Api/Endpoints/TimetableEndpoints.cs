using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Calendar.Application.Commands.Timetables;
using SchoolConnect.Calendar.Application.Queries.Timetables;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Api.Endpoints;

public static class TimetableEndpoints
{
    public static void MapTimetableEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/timetables")
            .WithTags("Timetables")
            .WithOpenApi();

        // GET /api/timetables/{id}
        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var query = new GetTimetableByIdQuery(id);
            var result = await mediator.Send(query);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetTimetableById")
        .Produces<TimetableDto>()
        .Produces(404);

        // POST /api/institutes/{instituteId}/timetables
        app.MapPost("/api/institutes/{instituteId:guid}/timetables", 
            async (Guid instituteId, [FromBody] CreateTimetableCommand command, IMediator mediator) =>
        {
            if (instituteId != command.InstituteId)
                return Results.BadRequest("Institute ID mismatch");

            var result = await mediator.Send(command);
            return Results.CreatedAtRoute("GetTimetableById", new { id = result.Id }, result);
        })
        .WithTags("Timetables")
        .WithName("CreateTimetable")
        .Produces<TimetableDto>(201);

        // POST /api/timetables/{id}/publish
        group.MapPost("/{id:guid}/publish", async (Guid id, [FromQuery] Guid publishedBy, IMediator mediator) =>
        {
            var command = new PublishTimetableCommand(id, publishedBy);
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("PublishTimetable")
        .Produces(204);
    }
}

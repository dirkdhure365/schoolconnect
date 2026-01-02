using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Calendar.Application.Commands.Timetables;
using SchoolConnect.Calendar.Application.Queries.Timetables;

namespace SchoolConnect.Calendar.Api.Endpoints;

public static class TimetableEndpoints
{
    public static void MapTimetableEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/timetables").WithTags("Timetables");

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTimetableByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateTimetableCommand command, IMediator mediator) =>
        {
            if (id != command.TimetableId)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapPost("/{id}/publish", async (Guid id, [FromBody] PublishTimetableCommand command, IMediator mediator) =>
        {
            if (id != command.TimetableId)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteTimetableCommand(id));
            return Results.NoContent();
        });

        group.MapGet("/{id}/slots", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTimetableSlotsQuery(id));
            return Results.Ok(result);
        });

        group.MapPost("/{id}/slots", async (Guid id, [FromBody] CreateSlotCommand command, IMediator mediator) =>
        {
            if (id != command.TimetableId)
                return Results.BadRequest("ID mismatch");
            
            var slotId = await mediator.Send(command);
            return Results.Created($"/api/timetables/{id}/slots/{slotId}", new { Id = slotId });
        });

        // Institute-specific timetables
        var instituteGroup = app.MapGroup("/api/institutes/{instituteId}/timetables").WithTags("Timetables");

        instituteGroup.MapGet("", async (Guid instituteId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTimetablesByInstituteQuery(instituteId));
            return Results.Ok(result);
        });

        instituteGroup.MapPost("", async (Guid instituteId, [FromBody] CreateTimetableCommand command, IMediator mediator) =>
        {
            if (instituteId != command.InstituteId)
                return Results.BadRequest("Institute ID mismatch");
            
            var id = await mediator.Send(command);
            return Results.Created($"/api/timetables/{id}", new { Id = id });
        });

        // View-specific endpoints
        var viewGroup = app.MapGroup("/api/timetables/views").WithTags("Timetable Views");

        viewGroup.MapGet("/teacher/{teacherId}", async (
            Guid teacherId,
            [FromQuery] Guid timetableId,
            IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTeacherTimetableQuery(teacherId, timetableId));
            return Results.Ok(result);
        });

        viewGroup.MapGet("/class/{classId}", async (
            Guid classId,
            [FromQuery] Guid timetableId,
            IMediator mediator) =>
        {
            var result = await mediator.Send(new GetClassTimetableQuery(classId, timetableId));
            return Results.Ok(result);
        });
    }
}

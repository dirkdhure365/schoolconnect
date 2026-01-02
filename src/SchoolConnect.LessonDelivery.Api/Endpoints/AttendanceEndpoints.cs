using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.LessonDelivery.Application.Commands.Attendance;
using SchoolConnect.LessonDelivery.Application.Queries.Attendance;

namespace SchoolConnect.LessonDelivery.Api.Endpoints;

public static class AttendanceEndpoints
{
    public static void MapAttendanceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/lesson-sessions/{sessionId:guid}/attendance")
            .WithTags("Attendance");

        // GET /api/lesson-sessions/{sessionId}/attendance
        group.MapGet("", async (Guid sessionId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAttendanceBySessionQuery { SessionId = sessionId });
            return Results.Ok(result);
        })
        .WithName("GetAttendanceBySession")
        .WithOpenApi();

        // POST /api/lesson-sessions/{sessionId}/attendance
        group.MapPost("", async (Guid sessionId, [FromBody] RecordAttendanceCommand command, [FromServices] IMediator mediator) =>
        {
            if (sessionId != command.LessonSessionId)
                return Results.BadRequest("Session ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/attendance/{id}", new { id });
        })
        .WithName("RecordAttendance")
        .WithOpenApi();

        var attendanceGroup = app.MapGroup("/api/attendance")
            .WithTags("Attendance");

        // PUT /api/attendance/{id}
        attendanceGroup.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateAttendanceCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateAttendance")
        .WithOpenApi();
    }
}

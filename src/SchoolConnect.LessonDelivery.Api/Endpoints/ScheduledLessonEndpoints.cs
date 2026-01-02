using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.LessonDelivery.Application.Commands.Scheduling;
using SchoolConnect.LessonDelivery.Application.Queries.Scheduling;

namespace SchoolConnect.LessonDelivery.Api.Endpoints;

public static class ScheduledLessonEndpoints
{
    public static void MapScheduledLessonEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/classes/{classId:guid}/scheduled-lessons")
            .WithTags("Scheduled Lessons");

        // GET /api/classes/{classId}/scheduled-lessons
        group.MapGet("", async (Guid classId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetScheduledLessonsByClassQuery { ClassId = classId });
            return Results.Ok(result);
        })
        .WithName("GetScheduledLessonsByClass")
        .WithOpenApi();

        // POST /api/classes/{classId}/scheduled-lessons
        group.MapPost("", async (Guid classId, [FromBody] ScheduleLessonCommand command, [FromServices] IMediator mediator) =>
        {
            if (classId != command.ClassId)
                return Results.BadRequest("Class ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/scheduled-lessons/{id}", new { id });
        })
        .WithName("ScheduleLesson")
        .WithOpenApi();

        var lessonGroup = app.MapGroup("/api/scheduled-lessons")
            .WithTags("Scheduled Lessons");

        // POST /api/scheduled-lessons/{id}/reschedule
        lessonGroup.MapPost("{id:guid}/reschedule", async (Guid id, [FromBody] RescheduleLessonCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("RescheduleLesson")
        .WithOpenApi();

        // POST /api/scheduled-lessons/{id}/cancel
        lessonGroup.MapPost("{id:guid}/cancel", async (Guid id, [FromBody] CancelLessonCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("CancelLesson")
        .WithOpenApi();
    }
}

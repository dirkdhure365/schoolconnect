using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.LessonDelivery.Application.Commands.Sessions;
using SchoolConnect.LessonDelivery.Application.Queries.Sessions;

namespace SchoolConnect.LessonDelivery.Api.Endpoints;

public static class LessonSessionEndpoints
{
    public static void MapLessonSessionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/scheduled-lessons/{scheduledLessonId:guid}")
            .WithTags("Lesson Sessions");

        // POST /api/scheduled-lessons/{scheduledLessonId}/start
        group.MapPost("/start", async (Guid scheduledLessonId, [FromBody] StartLessonCommand command, [FromServices] IMediator mediator) =>
        {
            if (scheduledLessonId != command.ScheduledLessonId)
                return Results.BadRequest("Scheduled lesson ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/lesson-sessions/{id}", new { id });
        })
        .WithName("StartLesson")
        .WithOpenApi();

        var sessionGroup = app.MapGroup("/api/lesson-sessions")
            .WithTags("Lesson Sessions");

        // GET /api/lesson-sessions/{id}
        sessionGroup.MapGet("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetLessonSessionByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetLessonSessionById")
        .WithOpenApi();

        // POST /api/lesson-sessions/{id}/end
        sessionGroup.MapPost("{id:guid}/end", async (Guid id, [FromBody] EndLessonCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("EndLesson")
        .WithOpenApi();

        // POST /api/lesson-sessions/{id}/artifacts
        sessionGroup.MapPost("{id:guid}/artifacts", async (Guid id, [FromBody] AddLessonArtifactCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.LessonSessionId)
                return Results.BadRequest("Lesson session ID mismatch");

            var artifactId = await mediator.Send(command);
            return Results.Created($"/api/lesson-artifacts/{artifactId}", new { id = artifactId });
        })
        .WithName("AddLessonArtifact")
        .WithOpenApi();
    }
}

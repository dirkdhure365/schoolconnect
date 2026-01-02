using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.LessonDelivery.Application.Commands.Homework;
using SchoolConnect.LessonDelivery.Application.Queries.Homework;

namespace SchoolConnect.LessonDelivery.Api.Endpoints;

public static class HomeworkEndpoints
{
    public static void MapHomeworkEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/classes/{classId:guid}/homework")
            .WithTags("Homework");

        // GET /api/classes/{classId}/homework
        group.MapGet("", async (Guid classId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetHomeworkByClassQuery { ClassId = classId });
            return Results.Ok(result);
        })
        .WithName("GetHomeworkByClass")
        .WithOpenApi();

        // POST /api/classes/{classId}/homework
        group.MapPost("", async (Guid classId, [FromBody] AssignHomeworkCommand command, [FromServices] IMediator mediator) =>
        {
            if (classId != command.ClassId)
                return Results.BadRequest("Class ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/homework/{id}", new { id });
        })
        .WithName("AssignHomework")
        .WithOpenApi();

        var homeworkGroup = app.MapGroup("/api/homework")
            .WithTags("Homework");

        // POST /api/homework/{id}/submit
        homeworkGroup.MapPost("{id:guid}/submit", async (Guid id, [FromBody] SubmitHomeworkCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.HomeworkId)
                return Results.BadRequest("Homework ID mismatch");

            var submissionId = await mediator.Send(command);
            return Results.Created($"/api/homework-submissions/{submissionId}", new { id = submissionId });
        })
        .WithName("SubmitHomework")
        .WithOpenApi();

        var submissionGroup = app.MapGroup("/api/homework-submissions")
            .WithTags("Homework");

        // POST /api/homework-submissions/{id}/grade
        submissionGroup.MapPost("{id:guid}/grade", async (Guid id, [FromBody] GradeHomeworkCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.SubmissionId)
                return Results.BadRequest("Submission ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("GradeHomework")
        .WithOpenApi();
    }
}

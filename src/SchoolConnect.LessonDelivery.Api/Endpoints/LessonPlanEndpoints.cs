using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;
using SchoolConnect.LessonDelivery.Application.Queries.LessonPlans;

namespace SchoolConnect.LessonDelivery.Api.Endpoints;

public static class LessonPlanEndpoints
{
    public static void MapLessonPlanEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/classes/{classId:guid}/lesson-plans")
            .WithTags("Lesson Plans");

        // GET /api/classes/{classId}/lesson-plans
        group.MapGet("", async (Guid classId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetLessonPlansByClassQuery { ClassId = classId });
            return Results.Ok(result);
        })
        .WithName("GetLessonPlansByClass")
        .WithOpenApi();

        // POST /api/classes/{classId}/lesson-plans
        group.MapPost("", async (Guid classId, [FromBody] CreateLessonPlanCommand command, [FromServices] IMediator mediator) =>
        {
            if (classId != command.ClassId)
                return Results.BadRequest("Class ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/lesson-plans/{id}", new { id });
        })
        .WithName("CreateLessonPlan")
        .WithOpenApi();

        var planGroup = app.MapGroup("/api/lesson-plans")
            .WithTags("Lesson Plans");

        // GET /api/lesson-plans/{id}
        planGroup.MapGet("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetLessonPlanByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetLessonPlanById")
        .WithOpenApi();

        // PUT /api/lesson-plans/{id}
        planGroup.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateLessonPlanCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateLessonPlan")
        .WithOpenApi();

        // DELETE /api/lesson-plans/{id}
        planGroup.MapDelete("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new DeleteLessonPlanCommand { Id = id });
            return Results.NoContent();
        })
        .WithName("DeleteLessonPlan")
        .WithOpenApi();

        // POST /api/lesson-plans/{id}/submit-for-approval
        planGroup.MapPost("{id:guid}/submit-for-approval", async (Guid id, [FromBody] SubmitLessonPlanForApprovalCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("SubmitLessonPlanForApproval")
        .WithOpenApi();

        // POST /api/lesson-plans/{id}/approve
        planGroup.MapPost("{id:guid}/approve", async (Guid id, [FromBody] ApproveLessonPlanCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("ApproveLessonPlan")
        .WithOpenApi();

        // POST /api/lesson-plans/{id}/reject
        planGroup.MapPost("{id:guid}/reject", async (Guid id, [FromBody] RejectLessonPlanCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("RejectLessonPlan")
        .WithOpenApi();
    }
}

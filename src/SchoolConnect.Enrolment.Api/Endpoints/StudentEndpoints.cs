using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Enrolment.Application.Commands.Students;
using SchoolConnect.Enrolment.Application.Queries.Students;

namespace SchoolConnect.Enrolment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/institutes/{instituteId:guid}/students")
            .WithTags("Students");

        // GET /api/institutes/{instituteId}/students - List students
        group.MapGet("", async (Guid instituteId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetStudentsByInstituteQuery { InstituteId = instituteId });
            return Results.Ok(result);
        })
        .WithName("GetStudents")
        .WithOpenApi();

        // POST /api/institutes/{instituteId}/students - Create student
        group.MapPost("", async (Guid instituteId, [FromBody] CreateStudentCommand command, [FromServices] IMediator mediator) =>
        {
            if (instituteId != command.InstituteId)
                return Results.BadRequest("Institute ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/students/{id}", new { id });
        })
        .WithName("CreateStudent")
        .WithOpenApi();

        var studentGroup = app.MapGroup("/api/students")
            .WithTags("Students");

        // GET /api/students/{id} - Get student by ID
        studentGroup.MapGet("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetStudentByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi();

        // PUT /api/students/{id} - Update student
        studentGroup.MapPut("{id:guid}", async (Guid id, [FromBody] UpdateStudentCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        // POST /api/students/{id}/withdraw - Withdraw student
        studentGroup.MapPost("{id:guid}/withdraw", async (Guid id, [FromBody] WithdrawStudentCommand command, [FromServices] IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("WithdrawStudent")
        .WithOpenApi();
    }
}

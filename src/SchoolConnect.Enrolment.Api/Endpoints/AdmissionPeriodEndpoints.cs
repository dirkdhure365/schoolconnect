using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Enrolment.Application.Commands.Admissions;
using SchoolConnect.Enrolment.Application.Queries.Admissions;

namespace SchoolConnect.Enrolment.Api.Endpoints;

public static class AdmissionPeriodEndpoints
{
    public static void MapAdmissionPeriodEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/institutes/{instituteId:guid}/admission-periods")
            .WithTags("Admission Periods");

        // GET /api/institutes/{instituteId}/admission-periods - List admission periods
        group.MapGet("", async (Guid instituteId, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAdmissionPeriodsByInstituteQuery { InstituteId = instituteId });
            return Results.Ok(result);
        })
        .WithName("GetAdmissionPeriods")
        .WithOpenApi();

        // POST /api/institutes/{instituteId}/admission-periods - Create admission period
        group.MapPost("", async (Guid instituteId, [FromBody] CreateAdmissionPeriodCommand command, [FromServices] IMediator mediator) =>
        {
            if (instituteId != command.InstituteId)
                return Results.BadRequest("Institute ID mismatch");

            var id = await mediator.Send(command);
            return Results.Created($"/api/admission-periods/{id}", new { id });
        })
        .WithName("CreateAdmissionPeriod")
        .WithOpenApi();

        var periodGroup = app.MapGroup("/api/admission-periods")
            .WithTags("Admission Periods");

        // GET /api/admission-periods/{id} - Get admission period by ID
        periodGroup.MapGet("{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAdmissionPeriodByIdQuery { Id = id });
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetAdmissionPeriodById")
        .WithOpenApi();

        // POST /api/admission-periods/{id}/open - Open admission period
        periodGroup.MapPost("{id:guid}/open", async (Guid id, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new OpenAdmissionPeriodCommand { Id = id });
            return Results.NoContent();
        })
        .WithName("OpenAdmissionPeriod")
        .WithOpenApi();

        // POST /api/admission-periods/{id}/close - Close admission period
        periodGroup.MapPost("{id:guid}/close", async (Guid id, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new CloseAdmissionPeriodCommand { Id = id });
            return Results.NoContent();
        })
        .WithName("CloseAdmissionPeriod")
        .WithOpenApi();
    }
}

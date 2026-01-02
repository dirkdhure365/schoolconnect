using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Calendar.Application.Commands.Timetables;

namespace SchoolConnect.Calendar.Api.Endpoints;

public static class TimetableSlotEndpoints
{
    public static void MapTimetableSlotEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/timetable-slots").WithTags("Timetable Slots");

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateSlotCommand command, IMediator mediator) =>
        {
            if (id != command.SlotId)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteSlotCommand(id));
            return Results.NoContent();
        });

        group.MapPost("/{id}/substitutions", async (Guid id, [FromBody] CreateSubstitutionCommand command, IMediator mediator) =>
        {
            if (id != command.TimetableSlotId)
                return Results.BadRequest("ID mismatch");
            
            var substitutionId = await mediator.Send(command);
            return Results.Created($"/api/timetable-changes/{substitutionId}", new { Id = substitutionId });
        });
    }
}

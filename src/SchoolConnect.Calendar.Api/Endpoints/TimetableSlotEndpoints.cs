using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Calendar.Application.Commands.Timetables;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Application.Queries.Timetables;

namespace SchoolConnect.Calendar.Api.Endpoints;

public static class TimetableSlotEndpoints
{
    public static void MapTimetableSlotEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/timetables/{timetableId:guid}/slots")
            .WithTags("Timetable Slots")
            .WithOpenApi();

        // GET /api/timetables/{timetableId}/slots
        group
            .MapGet(
                "/",
                async (Guid timetableId, [FromQuery] DayOfWeek? dayOfWeek, IMediator mediator) =>
                {
                    // Replace the instantiation of GetTimetableSlotsQuery to use the required constructor parameter
                    var query = new GetTimetableSlotsQuery(timetableId);
                    var result = await mediator.Send(query);
                    return Results.Ok(result);
                }
            )
            .WithName("GetTimetableSlots")
            .Produces<IEnumerable<TimetableSlotDto>>();

        // POST /api/timetables/{timetableId}/slots
        group
            .MapPost(
                "/",
                async (
                    Guid timetableId,
                    [FromBody] CreateSlotCommand command,
                    IMediator mediator
                ) =>
                {
                    if (timetableId != command.TimetableId)
                        return Results.BadRequest("Timetable ID mismatch");

                    var result = await mediator.Send(command);
                    return Results.Created($"/api/timetable-slots/{result.Id}", result);
                }
            )
            .WithName("CreateTimetableSlot")
            .Produces<TimetableSlotDto>(201);
    }
}

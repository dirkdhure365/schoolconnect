using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Calendar.Application.Commands.Events;
using SchoolConnect.Calendar.Application.Queries.Events;

namespace SchoolConnect.Calendar.Api.Endpoints;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/calendar/events").WithTags("Calendar Events");

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetEventByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapGet("", async (
            [FromQuery] Guid? instituteId,
            [FromQuery] Guid? centreId,
            [FromQuery] Guid? classId,
            IMediator mediator) =>
        {
            var result = await mediator.Send(new GetEventsQuery(instituteId, centreId, classId));
            return Results.Ok(result);
        });

        group.MapGet("/range", async (
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] Guid? instituteId,
            IMediator mediator) =>
        {
            var result = await mediator.Send(new GetEventsByDateRangeQuery(startDate, endDate, instituteId));
            return Results.Ok(result);
        });

        group.MapGet("/upcoming", async (
            IMediator mediator,
            [FromQuery] Guid userId,
            [FromQuery] int count = 10) =>
        {
            var result = await mediator.Send(new GetUpcomingEventsQuery(userId, count));
            return Results.Ok(result);
        });

        group.MapPost("", async ([FromBody] CreateEventCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/calendar/events/{id}", new { Id = id });
        });

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateEventCommand command, IMediator mediator) =>
        {
            if (id != command.EventId)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapPost("/{id}/cancel", async (Guid id, [FromBody] CancelEventCommand command, IMediator mediator) =>
        {
            if (id != command.EventId)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteEventCommand(id));
            return Results.NoContent();
        });

        group.MapPost("/{id}/attendees", async (Guid id, [FromBody] AddAttendeeCommand command, IMediator mediator) =>
        {
            if (id != command.EventId)
                return Results.BadRequest("ID mismatch");
            
            var attendeeId = await mediator.Send(command);
            return Results.Created($"/api/calendar/events/{id}/attendees/{attendeeId}", new { Id = attendeeId });
        });

        group.MapDelete("/{eventId}/attendees/{userId}", async (Guid eventId, Guid userId, IMediator mediator) =>
        {
            await mediator.Send(new RemoveAttendeeCommand(eventId, userId));
            return Results.NoContent();
        });
    }
}

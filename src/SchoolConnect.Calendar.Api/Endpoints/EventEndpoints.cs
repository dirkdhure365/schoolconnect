using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Calendar.Application.Commands.Events;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Application.Queries.Events;

namespace SchoolConnect.Calendar.Api.Endpoints;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/calendar/events").WithTags("Events").WithOpenApi();

        // GET /api/calendar/events/{id}
        group
            .MapGet(
                "/{id:guid}",
                async (Guid id, IMediator mediator) =>
                {
                    var query = new GetEventByIdQuery(id);
                    var result = await mediator.Send(query);
                    return result is not null ? Results.Ok(result) : Results.NotFound();
                }
            )
            .WithName("GetEventById")
            .Produces<CalendarEventDto>()
            .Produces(404);

        // GET /api/calendar/events/range
        group
            .MapGet(
                "/range",
                async (
                    [FromQuery] DateTime startDate,
                    [FromQuery] DateTime endDate,
                    [FromQuery] Guid? instituteId,
                    [FromQuery] Guid? centreId,
                    IMediator mediator
                ) =>
                {
                    // 'GetEventsByDateRangeQuery' does not have a 'CentreId' property, so do not set it
                    var query = new GetEventsByDateRangeQuery(startDate, endDate, instituteId);
                    var result = await mediator.Send(query);
                    return Results.Ok(result);
                }
            )
            .WithName("GetEventsByDateRange")
            .Produces<IEnumerable<CalendarEventDto>>();

        // POST /api/calendar/events
        group
            .MapPost(
                "/",
                async ([FromBody] CreateEventCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return Results.CreatedAtRoute("GetEventById", new { id = result }, result);
                }
            )
            .WithName("CreateEvent")
            .Produces<CalendarEventDto>(201);

        // PUT /api/calendar/events/{id}
        group
            .MapPut(
                "/{id:guid}",
                async (Guid id, [FromBody] UpdateEventCommand command, IMediator mediator) =>
                {
                    if (id != command.EventId)
                        return Results.BadRequest("Event ID mismatch");

                    var result = await mediator.Send(command);
                    return Results.Ok(result);
                }
            )
            .WithName("UpdateEvent")
            .Produces<CalendarEventDto>();

        // POST /api/calendar/events/{id}/cancel
        group
            .MapPost(
                "/{id:guid}/cancel",
                async (Guid id, [FromBody] CancelEventCommand command, IMediator mediator) =>
                {
                    if (id != command.EventId)
                        return Results.BadRequest("Event ID mismatch");

                    await mediator.Send(command);
                    return Results.NoContent();
                }
            )
            .WithName("CancelEvent")
            .Produces(204);

        // DELETE /api/calendar/events/{id}
        group
            .MapDelete(
                "/{id:guid}",
                async (Guid id, [FromQuery] Guid deletedBy, IMediator mediator) =>
                {
                    var command = new DeleteEventCommand(id);
                    await mediator.Send(command);
                    return Results.NoContent();
                }
            )
            .WithName("DeleteEvent")
            .Produces(204);

        // POST /api/calendar/events/{id}/rsvp
        group
            .MapPost(
                "/{id:guid}/rsvp",
                async (Guid id, [FromBody] RsvpEventCommand command, IMediator mediator) =>
                {
                    if (id != command.EventId)
                        return Results.BadRequest("Event ID mismatch");

                    await mediator.Send(command);
                    return Results.NoContent();
                }
            )
            .WithName("RsvpEvent")
            .Produces(204);
    }
}

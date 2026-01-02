using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Communication.Application.Commands.Notifications;
using SchoolConnect.Communication.Application.Queries.Notifications;

namespace SchoolConnect.Communication.Api.Endpoints;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notifications").WithTags("Notifications");

        group.MapGet("/", async ([FromQuery] Guid userId, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetNotificationsQuery(userId, page, pageSize));
            return Results.Ok(result);
        });

        group.MapGet("/unread-count", async ([FromQuery] Guid userId, IMediator mediator) =>
        {
            var count = await mediator.Send(new GetUnreadNotificationsCountQuery(userId));
            return Results.Ok(new { Count = count });
        });

        group.MapPost("/send", async ([FromBody] SendNotificationCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/notifications/{id}", new { Id = id });
        });

        group.MapPost("/{id}/read", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new MarkNotificationAsReadCommand(id));
            return Results.NoContent();
        });

        group.MapPost("/read-all", async ([FromBody] Guid userId, IMediator mediator) =>
        {
            await mediator.Send(new MarkAllNotificationsAsReadCommand(userId));
            return Results.NoContent();
        });
    }
}

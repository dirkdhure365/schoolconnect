using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Communication.Application.Commands.Messages;
using SchoolConnect.Communication.Application.Queries.Messages;

namespace SchoolConnect.Communication.Api.Endpoints;

public static class MessageEndpoints
{
    public static void MapMessageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/messages").WithTags("Messages");

        group.MapGet("/conversations", async ([FromQuery] Guid userId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetConversationsQuery(userId));
            return Results.Ok(result);
        });

        group.MapGet("/conversations/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetConversationByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapGet("/conversations/{id}/messages", async (Guid id, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetConversationMessagesQuery(id, page, pageSize));
            return Results.Ok(result);
        });

        group.MapPost("/send", async ([FromBody] SendMessageCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/messages/{id}", new { Id = id });
        });

        group.MapPost("/{id}/read", async (Guid id, [FromBody] Guid userId, IMediator mediator) =>
        {
            await mediator.Send(new MarkMessageAsReadCommand(id, userId));
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (Guid id, [FromBody] Guid userId, IMediator mediator) =>
        {
            await mediator.Send(new DeleteMessageCommand(id, userId));
            return Results.NoContent();
        });

        group.MapGet("/unread-count", async ([FromQuery] Guid userId, IMediator mediator) =>
        {
            var count = await mediator.Send(new GetUnreadCountQuery(userId));
            return Results.Ok(new { Count = count });
        });
    }
}

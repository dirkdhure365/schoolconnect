using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Collaboration.Application.Commands.Cards;
using SchoolConnect.Collaboration.Application.Queries.Cards;

namespace SchoolConnect.Collaboration.Api.Endpoints;

public static class CardEndpoints
{
    public static void MapCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cards").WithTags("Cards");

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCardByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapPost("", async ([FromBody] CreateCardCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/cards/{id}", new { Id = id });
        });

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateCardCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapPut("/{id}/move", async (Guid id, [FromBody] MoveCardCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        var listGroup = app.MapGroup("/api/lists/{listId}/cards").WithTags("Cards");

        listGroup.MapGet("", async (Guid listId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCardsByListQuery(listId));
            return Results.Ok(result);
        });
    }
}

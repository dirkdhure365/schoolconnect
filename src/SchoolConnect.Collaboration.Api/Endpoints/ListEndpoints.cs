using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Collaboration.Application.Commands.Lists;
using SchoolConnect.Collaboration.Application.Queries.Lists;

namespace SchoolConnect.Collaboration.Api.Endpoints;

public static class ListEndpoints
{
    public static void MapListEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/lists").WithTags("Lists");

        group.MapPost("", async ([FromBody] CreateListCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/lists/{id}", new { Id = id });
        });

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateListCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        var boardGroup = app.MapGroup("/api/boards/{boardId}/lists").WithTags("Lists");

        boardGroup.MapGet("", async (Guid boardId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetListsByBoardQuery(boardId));
            return Results.Ok(result);
        });
    }
}

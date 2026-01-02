using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Collaboration.Application.Commands.Boards;
using SchoolConnect.Collaboration.Application.Queries.Boards;

namespace SchoolConnect.Collaboration.Api.Endpoints;

public static class BoardEndpoints
{
    public static void MapBoardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/boards").WithTags("Boards");

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetBoardByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapPost("", async ([FromBody] CreateBoardCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/boards/{id}", new { Id = id });
        });

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateBoardCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        var workspaceGroup = app.MapGroup("/api/workspaces/{workspaceId}/boards").WithTags("Boards");

        workspaceGroup.MapGet("", async (Guid workspaceId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetBoardsByWorkspaceQuery(workspaceId));
            return Results.Ok(result);
        });
    }
}

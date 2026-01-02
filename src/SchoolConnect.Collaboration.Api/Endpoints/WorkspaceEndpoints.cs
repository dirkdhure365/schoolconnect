using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Collaboration.Application.Commands.Workspaces;
using SchoolConnect.Collaboration.Application.Queries.Workspaces;

namespace SchoolConnect.Collaboration.Api.Endpoints;

public static class WorkspaceEndpoints
{
    public static void MapWorkspaceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workspaces").WithTags("Workspaces");

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetWorkspaceByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapPost("", async ([FromBody] CreateWorkspaceCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/workspaces/{id}", new { Id = id });
        });

        group.MapPut("/{id}", async (Guid id, [FromBody] UpdateWorkspaceCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest("ID mismatch");
            
            await mediator.Send(command);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteWorkspaceCommand(id));
            return Results.NoContent();
        });

        group.MapPost("/{id}/members", async (Guid id, [FromBody] AddWorkspaceMemberCommand command, IMediator mediator) =>
        {
            if (id != command.WorkspaceId)
                return Results.BadRequest("ID mismatch");
            
            var memberId = await mediator.Send(command);
            return Results.Created($"/api/workspaces/{id}/members/{memberId}", new { Id = memberId });
        });
    }
}

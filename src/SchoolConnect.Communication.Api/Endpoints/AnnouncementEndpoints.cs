using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Communication.Application.Commands.Announcements;
using SchoolConnect.Communication.Application.Queries.Announcements;

namespace SchoolConnect.Communication.Api.Endpoints;

public static class AnnouncementEndpoints
{
    public static void MapAnnouncementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/announcements").WithTags("Announcements");

        group.MapGet("/institutes/{instituteId}", async (Guid instituteId, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAnnouncementsQuery(instituteId, page, pageSize));
            return Results.Ok(result);
        });

        group.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAnnouncementByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        });

        group.MapPost("/", async ([FromBody] CreateAnnouncementCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/announcements/{id}", new { Id = id });
        });

        group.MapPost("/{id}/publish", async (Guid id, [FromBody] int targetCount, IMediator mediator) =>
        {
            await mediator.Send(new PublishAnnouncementCommand(id, targetCount));
            return Results.NoContent();
        });

        group.MapPost("/{id}/acknowledge", async (Guid id, [FromBody] AcknowledgeAnnouncementCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        });
    }
}

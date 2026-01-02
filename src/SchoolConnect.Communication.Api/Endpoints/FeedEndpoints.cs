using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Communication.Application.Queries.Feed;

namespace SchoolConnect.Communication.Api.Endpoints;

public static class FeedEndpoints
{
    public static void MapFeedEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/feed").WithTags("Feed");

        group.MapGet("/", async ([FromQuery] Guid userId, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetFeedQuery(userId, page, pageSize));
            return Results.Ok(result);
        });
    }
}

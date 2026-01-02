using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Curriculum.Application.Services;

namespace SchoolConnect.Curriculum.Api.Endpoints;

/// <summary>
/// Endpoints for discovering available curriculum boards.
/// </summary>
public static class CurriculumDiscoveryEndpoints
{
    public static void MapCurriculumDiscoveryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/curriculum")
            .WithTags("Curriculum Discovery");

        // GET /api/curriculum/boards - List all registered boards
        group.MapGet("/boards", async (ICurriculumServiceFactory factory) =>
        {
            var boards = await factory.GetBoardsAsync();
            return Results.Ok(boards);
        })
        .WithName("GetAllBoards")
        .WithSummary("Get all registered curriculum boards")
        .Produces<IEnumerable<BoardInfo>>(200);

        // GET /api/curriculum/boards/country/{country} - Get boards by country
        group.MapGet("/boards/country/{country}", async (string country, ICurriculumServiceFactory factory) =>
        {
            var boards = await factory.GetBoardsByCountryAsync(country);
            return Results.Ok(boards);
        })
        .WithName("GetBoardsByCountry")
        .WithSummary("Get curriculum boards for a specific country")
        .Produces<IEnumerable<BoardInfo>>(200);
    }
}

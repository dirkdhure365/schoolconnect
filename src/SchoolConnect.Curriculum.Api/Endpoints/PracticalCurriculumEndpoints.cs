using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Curriculum.Application.Services;
using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Api.Endpoints;

/// <summary>
/// Endpoints for practical curriculum operations (PAT, projects, etc.).
/// </summary>
public static class PracticalCurriculumEndpoints
{
    public static void MapPracticalCurriculumEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/curriculum/{boardCode}/practical")
            .WithTags("Practical Curriculum");

        // GET /api/curriculum/{boardCode}/practical/subjects/{id}/grades/{grade}/pat
        group.MapGet("/subjects/{id}/grades/{grade}/pat", async (
            string boardCode,
            Guid id,
            int grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                
                if (service is not IPracticalCurriculumService practicalService)
                {
                    return Results.BadRequest(new { error = $"Board {boardCode} does not support practical assessments" });
                }

                var pat = await practicalService.GetPracticalAssessmentAsync(id, grade);

                if (pat == null)
                    return Results.NotFound(new { error = "Practical assessment not found" });

                return Results.Ok(pat);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetPracticalAssessment")
        .WithSummary("Get practical assessment (PAT) for a subject and grade")
        .Produces<IPracticalAssessment>(200)
        .Produces(400)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/practical/subjects/{id}/grades/{grade}/projects
        group.MapGet("/subjects/{id}/grades/{grade}/projects", async (
            string boardCode,
            Guid id,
            int grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                
                if (service is not IPracticalCurriculumService practicalService)
                {
                    return Results.BadRequest(new { error = $"Board {boardCode} does not support practical assessments" });
                }

                var projects = await practicalService.GetProjectsAsync(id, grade);
                return Results.Ok(projects);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetProjects")
        .WithSummary("Get projects for a subject and grade")
        .Produces<IEnumerable<IProject>>(200)
        .Produces(400)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/practical/subjects/{id}/grades/{grade}/programming-components
        group.MapGet("/subjects/{id}/grades/{grade}/programming-components", async (
            string boardCode,
            Guid id,
            int grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                
                if (service is not IPracticalCurriculumService practicalService)
                {
                    return Results.BadRequest(new { error = $"Board {boardCode} does not support practical assessments" });
                }

                var components = await practicalService.GetProgrammingComponentsAsync(id, grade);
                return Results.Ok(components);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetProgrammingComponents")
        .WithSummary("Get programming components for a subject and grade")
        .Produces<IEnumerable<IProgrammingComponent>>(200)
        .Produces(400)
        .Produces(404);
    }
}

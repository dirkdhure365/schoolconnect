using Microsoft.AspNetCore.Mvc;
using SchoolConnect.Curriculum.Application.Services;
using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Api.Endpoints;

/// <summary>
/// Endpoints for curriculum operations parameterized by board code.
/// </summary>
public static class CurriculumEndpoints
{
    public static void MapCurriculumEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/curriculum/{boardCode}")
            .WithTags("Curriculum");

        // GET /api/curriculum/{boardCode}/framework
        group.MapGet("/framework", async (string boardCode, ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                var framework = await service.GetFrameworkAsync();
                return Results.Ok(framework);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetFramework")
        .WithSummary("Get curriculum framework")
        .Produces<ICurriculumFramework>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/phases
        group.MapGet("/phases", async (string boardCode, ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                var phases = await service.GetPhasesAsync();
                return Results.Ok(phases);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetPhases")
        .WithSummary("Get educational phases")
        .Produces<IEnumerable<IEducationalPhase>>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/subjects
        group.MapGet("/subjects", async (
            string boardCode,
            [FromQuery] Guid? phaseId,
            [FromQuery] int? grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                IEnumerable<ISubject> subjects;

                if (phaseId.HasValue)
                {
                    subjects = await service.GetSubjectsByPhaseAsync(phaseId.Value);
                }
                else if (grade.HasValue)
                {
                    subjects = await service.GetSubjectsByGradeAsync(grade.Value);
                }
                else
                {
                    subjects = await service.GetSubjectsAsync();
                }

                return Results.Ok(subjects);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetSubjects")
        .WithSummary("Get subjects (optionally filtered by phase or grade)")
        .Produces<IEnumerable<ISubject>>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/subjects/{id}/topics
        group.MapGet("/subjects/{id}/topics", async (
            string boardCode,
            Guid id,
            [FromQuery] int? grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                IEnumerable<ITopic> topics;

                if (grade.HasValue)
                {
                    topics = await service.GetTopicsBySubjectAndGradeAsync(id, grade.Value);
                }
                else
                {
                    topics = await service.GetTopicsBySubjectAsync(id);
                }

                return Results.Ok(topics);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetTopics")
        .WithSummary("Get topics for a subject (optionally filtered by grade)")
        .Produces<IEnumerable<ITopic>>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/subjects/{id}/grades/{grade}/curriculum
        group.MapGet("/subjects/{id}/grades/{grade}/curriculum", async (
            string boardCode,
            Guid id,
            int grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                var curriculum = await service.GetGradeCurriculumAsync(id, grade);

                if (curriculum == null)
                    return Results.NotFound(new { error = "Grade curriculum not found" });

                return Results.Ok(curriculum);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetGradeCurriculum")
        .WithSummary("Get grade-specific curriculum")
        .Produces<IGradeCurriculum>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/subjects/{id}/assessment-policy
        group.MapGet("/subjects/{id}/assessment-policy", async (
            string boardCode,
            Guid id,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                var policy = await service.GetAssessmentPolicyAsync(id);

                if (policy == null)
                    return Results.NotFound(new { error = "Assessment policy not found" });

                return Results.Ok(policy);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetAssessmentPolicy")
        .WithSummary("Get assessment policy for a subject")
        .Produces<IAssessmentPolicy>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/subjects/{id}/glossary
        group.MapGet("/subjects/{id}/glossary", async (
            string boardCode,
            Guid id,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                var service = factory.CreateService(boardCode);
                var glossary = await service.GetGlossaryAsync(id);

                if (glossary == null)
                    return Results.NotFound(new { error = "Glossary not found" });

                return Results.Ok(glossary);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("GetGlossary")
        .WithSummary("Get glossary for a subject")
        .Produces<IGlossary>(200)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/search/content
        group.MapGet("/search/content", async (
            string boardCode,
            [FromQuery] string query,
            [FromQuery] int? grade,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return Results.BadRequest(new { error = "Query parameter is required" });

                var service = factory.CreateService(boardCode);
                var results = await service.SearchContentAsync(query, grade);
                return Results.Ok(results);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("SearchContent")
        .WithSummary("Search for content across the curriculum")
        .Produces<IEnumerable<IContentItem>>(200)
        .Produces(400)
        .Produces(404);

        // GET /api/curriculum/{boardCode}/search/objectives
        group.MapGet("/search/objectives", async (
            string boardCode,
            [FromQuery] string query,
            [FromQuery] CognitiveLevel? level,
            ICurriculumServiceFactory factory) =>
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return Results.BadRequest(new { error = "Query parameter is required" });

                var service = factory.CreateService(boardCode);
                var results = await service.SearchObjectivesAsync(query, level);
                return Results.Ok(results);
            }
            catch (ArgumentException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("SearchObjectives")
        .WithSummary("Search for learning objectives")
        .Produces<IEnumerable<ILearningObjective>>(200)
        .Produces(400)
        .Produces(404);
    }
}

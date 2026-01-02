using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Application.Services;

/// <summary>
/// Generic curriculum service interface for any curriculum framework.
/// </summary>
public interface ICurriculumService
{
    /// <summary>
    /// Gets the curriculum framework.
    /// </summary>
    Task<ICurriculumFramework> GetFrameworkAsync();

    /// <summary>
    /// Gets all phases in the curriculum.
    /// </summary>
    Task<IEnumerable<IEducationalPhase>> GetPhasesAsync();

    /// <summary>
    /// Gets a specific phase by ID.
    /// </summary>
    Task<IEducationalPhase?> GetPhaseByIdAsync(Guid phaseId);

    /// <summary>
    /// Gets all subjects in the curriculum.
    /// </summary>
    Task<IEnumerable<ISubject>> GetSubjectsAsync();

    /// <summary>
    /// Gets subjects for a specific phase.
    /// </summary>
    Task<IEnumerable<ISubject>> GetSubjectsByPhaseAsync(Guid phaseId);

    /// <summary>
    /// Gets subjects for a specific grade.
    /// </summary>
    Task<IEnumerable<ISubject>> GetSubjectsByGradeAsync(int grade);

    /// <summary>
    /// Gets a specific subject by ID.
    /// </summary>
    Task<ISubject?> GetSubjectByIdAsync(Guid subjectId);

    /// <summary>
    /// Gets topics for a subject.
    /// </summary>
    Task<IEnumerable<ITopic>> GetTopicsBySubjectAsync(Guid subjectId);

    /// <summary>
    /// Gets topics for a subject and grade.
    /// </summary>
    Task<IEnumerable<ITopic>> GetTopicsBySubjectAndGradeAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets a specific topic by ID.
    /// </summary>
    Task<ITopic?> GetTopicByIdAsync(Guid topicId);

    /// <summary>
    /// Gets grade curriculum for a subject and grade.
    /// </summary>
    Task<IGradeCurriculum?> GetGradeCurriculumAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets term plans for a subject and grade.
    /// </summary>
    Task<IEnumerable<ITermPlan>> GetTermPlansAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets assessment policy for a subject.
    /// </summary>
    Task<IAssessmentPolicy?> GetAssessmentPolicyAsync(Guid subjectId);

    /// <summary>
    /// Gets grade assessment requirements.
    /// </summary>
    Task<IGradeAssessmentRequirements?> GetGradeAssessmentRequirementsAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets resources for a subject.
    /// </summary>
    Task<IEnumerable<IResource>> GetResourcesAsync(Guid subjectId);

    /// <summary>
    /// Gets glossary for a subject.
    /// </summary>
    Task<IGlossary?> GetGlossaryAsync(Guid subjectId);

    /// <summary>
    /// Searches for content across the curriculum.
    /// </summary>
    Task<IEnumerable<IContentItem>> SearchContentAsync(string query, int? grade = null);

    /// <summary>
    /// Searches for learning objectives.
    /// </summary>
    Task<IEnumerable<ILearningObjective>> SearchObjectivesAsync(string query, CognitiveLevel? level = null);
}

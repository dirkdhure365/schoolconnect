using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Caps.Repositories;

/// <summary>
/// Repository interface for CAPS curriculum data.
/// </summary>
public interface ICapsRepository
{
    /// <summary>
    /// Gets the CAPS framework.
    /// </summary>
    Task<ICurriculumFramework> GetFrameworkAsync();

    /// <summary>
    /// Gets all phases.
    /// </summary>
    Task<IEnumerable<IEducationalPhase>> GetPhasesAsync();

    /// <summary>
    /// Gets a phase by ID.
    /// </summary>
    Task<IEducationalPhase?> GetPhaseByIdAsync(Guid phaseId);

    /// <summary>
    /// Gets all subjects.
    /// </summary>
    Task<IEnumerable<ISubject>> GetSubjectsAsync();

    /// <summary>
    /// Gets a subject by ID.
    /// </summary>
    Task<ISubject?> GetSubjectByIdAsync(Guid subjectId);

    /// <summary>
    /// Gets topics for a subject.
    /// </summary>
    Task<IEnumerable<ITopic>> GetTopicsBySubjectAsync(Guid subjectId);

    /// <summary>
    /// Gets a topic by ID.
    /// </summary>
    Task<ITopic?> GetTopicByIdAsync(Guid topicId);

    /// <summary>
    /// Gets grade curriculum.
    /// </summary>
    Task<IGradeCurriculum?> GetGradeCurriculumAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets practical assessment for a subject and grade.
    /// </summary>
    Task<IPracticalAssessment?> GetPracticalAssessmentAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets projects for a subject and grade.
    /// </summary>
    Task<IEnumerable<IProject>> GetProjectsAsync(Guid subjectId, int grade);
}

using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Zimsec.Repositories;

/// <summary>
/// Repository interface for ZIMSEC curriculum data.
/// </summary>
public interface IZimsecRepository
{
    /// <summary>
    /// Gets the ZIMSEC framework.
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
}

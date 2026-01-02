using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for project definitions in project-based subjects.
/// </summary>
public interface IProject
{
    /// <summary>
    /// Unique identifier for the project.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the project.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the project.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Subject this project belongs to.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Grade level for this project.
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// Term when project is undertaken.
    /// </summary>
    SchoolTerm Term { get; }

    /// <summary>
    /// Total marks allocated for the project.
    /// </summary>
    int TotalMarks { get; }

    /// <summary>
    /// Weight of the project in final assessment.
    /// </summary>
    decimal Weight { get; }

    /// <summary>
    /// Project phases or milestones.
    /// </summary>
    IReadOnlyCollection<IProjectPhase> Phases { get; }

    /// <summary>
    /// Assessment criteria for the project.
    /// </summary>
    IReadOnlyCollection<IProjectCriterion> Criteria { get; }

    /// <summary>
    /// Topics covered in the project.
    /// </summary>
    IReadOnlyCollection<Guid> TopicIds { get; }
}

/// <summary>
/// Represents a phase or milestone in a project.
/// </summary>
public interface IProjectPhase
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the phase.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of what should be completed in this phase.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Sequence order of the phase.
    /// </summary>
    int Sequence { get; }

    /// <summary>
    /// Recommended duration in weeks.
    /// </summary>
    int? DurationWeeks { get; }
}

/// <summary>
/// Represents assessment criteria for a project.
/// </summary>
public interface IProjectCriterion
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the criterion.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of what is being assessed.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Marks allocated for this criterion.
    /// </summary>
    int Marks { get; }

    /// <summary>
    /// Cognitive level assessed.
    /// </summary>
    CognitiveLevel? CognitiveLevel { get; }
}

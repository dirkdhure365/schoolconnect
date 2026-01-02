using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for practical assessments (e.g., PAT for IT subjects).
/// </summary>
public interface IPracticalAssessment
{
    /// <summary>
    /// Unique identifier for the practical assessment.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the assessment (e.g., "Practical Assessment Task").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the practical assessment.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Subject this assessment belongs to.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Grade level.
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// Total marks for the assessment.
    /// </summary>
    int TotalMarks { get; }

    /// <summary>
    /// Weight in final mark (percentage).
    /// </summary>
    decimal Weight { get; }

    /// <summary>
    /// Phases or stages of the practical assessment.
    /// </summary>
    IReadOnlyCollection<IPracticalPhase> Phases { get; }

    /// <summary>
    /// Programming components required.
    /// </summary>
    IReadOnlyCollection<IProgrammingComponent> ProgrammingComponents { get; }

    /// <summary>
    /// Assessment criteria.
    /// </summary>
    IReadOnlyCollection<IPracticalCriterion> Criteria { get; }

    /// <summary>
    /// Topics covered.
    /// </summary>
    IReadOnlyCollection<Guid> TopicIds { get; }
}

/// <summary>
/// Represents a phase in a practical assessment.
/// </summary>
public interface IPracticalPhase
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
    /// Description of the phase.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Term when this phase is conducted.
    /// </summary>
    SchoolTerm Term { get; }

    /// <summary>
    /// Marks allocated for this phase.
    /// </summary>
    int Marks { get; }

    /// <summary>
    /// Sequence order.
    /// </summary>
    int Sequence { get; }
}

/// <summary>
/// Represents a programming component required in practical assessment.
/// </summary>
public interface IProgrammingComponent
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the component.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Type of component.
    /// </summary>
    ComponentType Type { get; }

    /// <summary>
    /// Description of the component.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Whether this component is mandatory.
    /// </summary>
    bool IsMandatory { get; }

    /// <summary>
    /// Examples or hints.
    /// </summary>
    string? Examples { get; }
}

/// <summary>
/// Represents assessment criteria for practical work.
/// </summary>
public interface IPracticalCriterion
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
    /// Marks allocated.
    /// </summary>
    int Marks { get; }

    /// <summary>
    /// Cognitive level assessed.
    /// </summary>
    CognitiveLevel? CognitiveLevel { get; }
}

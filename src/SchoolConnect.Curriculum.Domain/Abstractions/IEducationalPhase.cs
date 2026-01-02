namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for educational phases (e.g., Foundation Phase, Intermediate Phase).
/// </summary>
public interface IEducationalPhase
{
    /// <summary>
    /// Unique identifier for the phase.
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
    /// Starting grade for this phase.
    /// </summary>
    int StartGrade { get; }

    /// <summary>
    /// Ending grade for this phase.
    /// </summary>
    int EndGrade { get; }

    /// <summary>
    /// Recommended minimum teaching hours per week.
    /// </summary>
    int MinimumTeachingHours { get; }

    /// <summary>
    /// Recommended maximum teaching hours per week.
    /// </summary>
    int MaximumTeachingHours { get; }

    /// <summary>
    /// Grade-specific curricula for this phase.
    /// </summary>
    IReadOnlyCollection<IGradeCurriculum> GradeCurricula { get; }

    /// <summary>
    /// Checks if a grade falls within this phase.
    /// </summary>
    /// <param name="grade">Grade to check.</param>
    /// <returns>True if grade is within phase range.</returns>
    bool ContainsGrade(int grade);
}

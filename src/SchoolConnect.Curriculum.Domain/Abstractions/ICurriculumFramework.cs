using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Base interface for any curriculum framework (CAPS, ZIMSEC, Cambridge, etc.).
/// </summary>
public interface ICurriculumFramework
{
    /// <summary>
    /// Unique identifier for the framework.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the curriculum framework.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Unique code for the framework (e.g., "CAPS", "ZIMSEC").
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Country where this curriculum is used.
    /// </summary>
    string Country { get; }

    /// <summary>
    /// Examination board or authority.
    /// </summary>
    string ExaminationBoard { get; }

    /// <summary>
    /// Version of the curriculum.
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Date when this curriculum became effective.
    /// </summary>
    DateTime EffectiveDate { get; }

    /// <summary>
    /// Description of the curriculum framework.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Educational phases in this curriculum (e.g., Foundation, Intermediate, Senior, FET).
    /// </summary>
    IReadOnlyCollection<IEducationalPhase> Phases { get; }

    /// <summary>
    /// All subjects offered in this curriculum.
    /// </summary>
    IReadOnlyCollection<ISubject> Subjects { get; }

    /// <summary>
    /// Core principles or values of the curriculum.
    /// </summary>
    IReadOnlyCollection<string> Principles { get; }

    /// <summary>
    /// General aims of the curriculum.
    /// </summary>
    IReadOnlyCollection<string> GeneralAims { get; }

    /// <summary>
    /// Gets subjects applicable to a specific phase.
    /// </summary>
    /// <param name="phaseId">Phase identifier.</param>
    /// <returns>Collection of subjects for the phase.</returns>
    IEnumerable<ISubject> GetSubjectsByPhase(Guid phaseId);

    /// <summary>
    /// Gets subjects applicable to a specific grade.
    /// </summary>
    /// <param name="grade">Grade level.</param>
    /// <returns>Collection of subjects for the grade.</returns>
    IEnumerable<ISubject> GetSubjectsByGrade(int grade);
}

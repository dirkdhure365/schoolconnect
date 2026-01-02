using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for subject assessment policy.
/// </summary>
public interface IAssessmentPolicy
{
    /// <summary>
    /// Unique identifier for the assessment policy.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Subject this policy applies to.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Description of the assessment approach.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Programme of assessment components.
    /// </summary>
    IReadOnlyCollection<IAssessmentComponent> Components { get; }

    /// <summary>
    /// Grading or achievement scales.
    /// </summary>
    IReadOnlyCollection<IAchievementScale> AchievementScales { get; }

    /// <summary>
    /// Annual programme of assessment.
    /// </summary>
    IProgrammeOfAssessment? ProgrammeOfAssessment { get; }
}

/// <summary>
/// Interface for grade-specific assessment requirements.
/// </summary>
public interface IGradeAssessmentRequirements
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Grade level.
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// Minimum number of formal assessments required.
    /// </summary>
    int MinimumFormalAssessments { get; }

    /// <summary>
    /// Required formal assessment tasks.
    /// </summary>
    IReadOnlyCollection<IFormalAssessmentTask> RequiredTasks { get; }

    /// <summary>
    /// Weighting of school-based assessment (SBA) percentage.
    /// </summary>
    decimal SchoolBasedAssessmentWeight { get; }

    /// <summary>
    /// Weighting of final examination percentage.
    /// </summary>
    decimal FinalExaminationWeight { get; }
}

/// <summary>
/// Interface for an assessment component (e.g., Paper 1, Paper 2, PAT).
/// </summary>
public interface IAssessmentComponent
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the component (e.g., "Paper 1", "Practical Assessment Task").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Type of assessment.
    /// </summary>
    AssessmentType Type { get; }

    /// <summary>
    /// Weight in final mark (percentage).
    /// </summary>
    decimal Weight { get; }

    /// <summary>
    /// Duration in minutes.
    /// </summary>
    int? DurationMinutes { get; }

    /// <summary>
    /// Total marks for this component.
    /// </summary>
    int TotalMarks { get; }

    /// <summary>
    /// Description of the component.
    /// </summary>
    string? Description { get; }
}

/// <summary>
/// Interface for a formal assessment task.
/// </summary>
public interface IFormalAssessmentTask
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the task.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Type of assessment.
    /// </summary>
    AssessmentType Type { get; }

    /// <summary>
    /// Term when this task is conducted.
    /// </summary>
    SchoolTerm Term { get; }

    /// <summary>
    /// Total marks for the task.
    /// </summary>
    int TotalMarks { get; }

    /// <summary>
    /// Weight in term or year mark.
    /// </summary>
    decimal? Weight { get; }

    /// <summary>
    /// Topics covered in this assessment.
    /// </summary>
    IReadOnlyCollection<Guid> TopicIds { get; }

    /// <summary>
    /// Duration in minutes.
    /// </summary>
    int? DurationMinutes { get; }
}

/// <summary>
/// Interface for programme of assessment (annual assessment plan).
/// </summary>
public interface IProgrammeOfAssessment
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Subject identifier.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Grade level.
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// Formal tasks throughout the year.
    /// </summary>
    IReadOnlyCollection<IFormalAssessmentTask> FormalTasks { get; }

    /// <summary>
    /// Description or guidelines.
    /// </summary>
    string? Description { get; }
}

/// <summary>
/// Interface for achievement or grading scale.
/// </summary>
public interface IAchievementScale
{
    /// <summary>
    /// Rating level (e.g., 1-7 for CAPS, A-E for A-Level).
    /// </summary>
    string Rating { get; }

    /// <summary>
    /// Description of the achievement level.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Minimum percentage for this rating.
    /// </summary>
    decimal MinPercentage { get; }

    /// <summary>
    /// Maximum percentage for this rating.
    /// </summary>
    decimal MaxPercentage { get; }
}

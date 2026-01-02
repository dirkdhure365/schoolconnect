using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for grade-specific curriculum.
/// </summary>
public interface IGradeCurriculum
{
    /// <summary>
    /// Unique identifier for the grade curriculum.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Subject this curriculum belongs to.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Grade level.
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// Academic year.
    /// </summary>
    int? Year { get; }

    /// <summary>
    /// Term plans for this grade.
    /// </summary>
    IReadOnlyCollection<ITermPlan> TermPlans { get; }

    /// <summary>
    /// Total teaching time allocation in hours per week.
    /// </summary>
    int WeeklyTeachingHours { get; }

    /// <summary>
    /// Assessment requirements for this grade.
    /// </summary>
    IGradeAssessmentRequirements? AssessmentRequirements { get; }
}

/// <summary>
/// Interface for a term plan.
/// </summary>
public interface ITermPlan
{
    /// <summary>
    /// Unique identifier for the term plan.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Term or semester.
    /// </summary>
    SchoolTerm Term { get; }

    /// <summary>
    /// Topics to be covered in this term.
    /// </summary>
    IReadOnlyCollection<ITermTopic> Topics { get; }

    /// <summary>
    /// Formal assessments for this term.
    /// </summary>
    IReadOnlyCollection<IFormalAssessmentTask> Assessments { get; }

    /// <summary>
    /// Number of teaching weeks in the term.
    /// </summary>
    int WeeksInTerm { get; }
}

/// <summary>
/// Represents a topic within a term plan.
/// </summary>
public interface ITermTopic
{
    /// <summary>
    /// Topic identifier.
    /// </summary>
    Guid TopicId { get; }

    /// <summary>
    /// Weeks allocated for this topic.
    /// </summary>
    int WeeksAllocated { get; }

    /// <summary>
    /// Starting week number in the term.
    /// </summary>
    int StartWeek { get; }

    /// <summary>
    /// Ending week number in the term.
    /// </summary>
    int EndWeek { get; }
}

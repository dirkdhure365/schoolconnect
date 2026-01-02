namespace SchoolConnect.Curriculum.Domain.Enums;

/// <summary>
/// Defines the types of subjects in a curriculum.
/// </summary>
public enum SubjectType
{
    /// <summary>
    /// Core/compulsory subjects required for all students.
    /// </summary>
    Core,

    /// <summary>
    /// Optional elective subjects.
    /// </summary>
    Elective,

    /// <summary>
    /// Vocational or technical subjects.
    /// </summary>
    Vocational,

    /// <summary>
    /// Language subjects.
    /// </summary>
    Language,

    /// <summary>
    /// Practical subjects with hands-on components.
    /// </summary>
    Practical,

    /// <summary>
    /// Composite subjects with multiple components.
    /// </summary>
    Composite,

    /// <summary>
    /// Life skills and orientation subjects.
    /// </summary>
    LifeSkills
}

using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for a subject in the curriculum.
/// </summary>
public interface ISubject
{
    /// <summary>
    /// Unique identifier for the subject.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the subject.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Unique code for the subject.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Type of subject.
    /// </summary>
    SubjectType Type { get; }

    /// <summary>
    /// Whether this subject is compulsory.
    /// </summary>
    bool IsCompulsory { get; }

    /// <summary>
    /// Description of the subject.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Phases where this subject is offered.
    /// </summary>
    IReadOnlyCollection<Guid> ApplicablePhaseIds { get; }

    /// <summary>
    /// Specific grades where this subject is offered.
    /// </summary>
    IReadOnlyCollection<int> ApplicableGrades { get; }

    /// <summary>
    /// Specific aims of the subject.
    /// </summary>
    IReadOnlyCollection<string> Aims { get; }

    /// <summary>
    /// Skills developed in this subject.
    /// </summary>
    IReadOnlyCollection<ISkill> Skills { get; }

    /// <summary>
    /// Key concepts covered in the subject.
    /// </summary>
    IReadOnlyCollection<IConcept> Concepts { get; }

    /// <summary>
    /// Main topics in the subject.
    /// </summary>
    IReadOnlyCollection<ITopic> Topics { get; }

    /// <summary>
    /// Resources required for teaching this subject.
    /// </summary>
    IReadOnlyCollection<IResource> Resources { get; }

    /// <summary>
    /// Assessment policy for the subject.
    /// </summary>
    IAssessmentPolicy? AssessmentPolicy { get; }

    /// <summary>
    /// For composite subjects, the component subjects.
    /// </summary>
    IReadOnlyCollection<ISubject>? Components { get; }

    /// <summary>
    /// Glossary of subject-specific terms.
    /// </summary>
    IGlossary? Glossary { get; }
}

/// <summary>
/// Represents a skill developed in a subject.
/// </summary>
public interface ISkill
{
    /// <summary>
    /// Unique identifier for the skill.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the skill.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the skill.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Category of the skill.
    /// </summary>
    SkillCategory Category { get; }
}

/// <summary>
/// Represents a key concept in a subject.
/// </summary>
public interface IConcept
{
    /// <summary>
    /// Unique identifier for the concept.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the concept.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the concept.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Related concepts.
    /// </summary>
    IReadOnlyCollection<Guid>? RelatedConceptIds { get; }
}

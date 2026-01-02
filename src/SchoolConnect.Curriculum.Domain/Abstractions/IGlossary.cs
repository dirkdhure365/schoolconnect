namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for subject-specific glossary of terms.
/// </summary>
public interface IGlossary
{
    /// <summary>
    /// Unique identifier for the glossary.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Subject this glossary belongs to.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Terms in the glossary.
    /// </summary>
    IReadOnlyCollection<IGlossaryTerm> Terms { get; }
}

/// <summary>
/// Represents a term in the glossary.
/// </summary>
public interface IGlossaryTerm
{
    /// <summary>
    /// Unique identifier for the term.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// The term or word.
    /// </summary>
    string Term { get; }

    /// <summary>
    /// Definition of the term.
    /// </summary>
    string Definition { get; }

    /// <summary>
    /// Additional context or usage examples.
    /// </summary>
    string? Context { get; }

    /// <summary>
    /// Related terms.
    /// </summary>
    IReadOnlyCollection<string>? RelatedTerms { get; }
}

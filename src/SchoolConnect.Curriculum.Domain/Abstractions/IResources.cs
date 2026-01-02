using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for teaching and learning resources.
/// </summary>
public interface IResource
{
    /// <summary>
    /// Unique identifier for the resource.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name or title of the resource.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Category of the resource.
    /// </summary>
    ResourceCategory Category { get; }

    /// <summary>
    /// Description of the resource.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Whether this resource is required or optional.
    /// </summary>
    bool IsRequired { get; }

    /// <summary>
    /// Publisher or provider of the resource.
    /// </summary>
    string? Provider { get; }

    /// <summary>
    /// URL or reference link.
    /// </summary>
    string? Url { get; }

    /// <summary>
    /// ISBN or identifier for textbooks.
    /// </summary>
    string? Isbn { get; }
}

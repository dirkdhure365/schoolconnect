using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Abstractions;

/// <summary>
/// Interface for a topic within a subject.
/// </summary>
public interface ITopic
{
    /// <summary>
    /// Unique identifier for the topic.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the topic.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Code or reference number for the topic.
    /// </summary>
    string? Code { get; }

    /// <summary>
    /// Description of the topic.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Subject this topic belongs to.
    /// </summary>
    Guid SubjectId { get; }

    /// <summary>
    /// Grades where this topic is covered.
    /// </summary>
    IReadOnlyCollection<int> ApplicableGrades { get; }

    /// <summary>
    /// Recommended teaching time in hours.
    /// </summary>
    int? RecommendedHours { get; }

    /// <summary>
    /// Weight of this topic in assessment (percentage).
    /// </summary>
    decimal? ContentWeighting { get; }

    /// <summary>
    /// Sub-topics under this topic.
    /// </summary>
    IReadOnlyCollection<ISubTopic> SubTopics { get; }

    /// <summary>
    /// Content items or learning units in this topic.
    /// </summary>
    IReadOnlyCollection<IContentItem> ContentItems { get; }

    /// <summary>
    /// Learning objectives for this topic.
    /// </summary>
    IReadOnlyCollection<ILearningObjective> LearningObjectives { get; }

    /// <summary>
    /// Topic progressions showing how the topic evolves across grades.
    /// </summary>
    IReadOnlyCollection<ITopicProgression> TopicProgressions { get; }

    /// <summary>
    /// Linked topics for cross-referencing.
    /// </summary>
    IReadOnlyCollection<Guid>? LinkedTopicIds { get; }
}

/// <summary>
/// Interface for a sub-topic within a topic.
/// </summary>
public interface ISubTopic
{
    /// <summary>
    /// Unique identifier for the sub-topic.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Name of the sub-topic.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the sub-topic.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Parent topic identifier.
    /// </summary>
    Guid TopicId { get; }

    /// <summary>
    /// Content items in this sub-topic.
    /// </summary>
    IReadOnlyCollection<IContentItem> ContentItems { get; }

    /// <summary>
    /// Learning objectives for this sub-topic.
    /// </summary>
    IReadOnlyCollection<ILearningObjective> LearningObjectives { get; }
}

/// <summary>
/// Represents a content item or learning unit.
/// </summary>
public interface IContentItem
{
    /// <summary>
    /// Unique identifier for the content item.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Title or name of the content.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Detailed description of the content.
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// Order or sequence within the topic/sub-topic.
    /// </summary>
    int Sequence { get; }

    /// <summary>
    /// Grade level for this content.
    /// </summary>
    int? Grade { get; }
}

/// <summary>
/// Represents a learning objective.
/// </summary>
public interface ILearningObjective
{
    /// <summary>
    /// Unique identifier for the objective.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Description of what learners should achieve.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Cognitive level of the objective.
    /// </summary>
    CognitiveLevel CognitiveLevel { get; }

    /// <summary>
    /// Skills associated with this objective.
    /// </summary>
    IReadOnlyCollection<Guid>? SkillIds { get; }
}

/// <summary>
/// Represents how a topic progresses across grades.
/// </summary>
public interface ITopicProgression
{
    /// <summary>
    /// Grade level.
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// Description of what is covered at this grade.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Cognitive depth expected at this grade.
    /// </summary>
    CognitiveLevel ExpectedLevel { get; }
}

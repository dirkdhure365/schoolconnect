using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Entity implementation of ITopic.
/// </summary>
public class TopicEntity : ITopic
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public string? Description { get; set; }
    public Guid SubjectId { get; set; }
    public int? RecommendedHours { get; set; }
    public decimal? ContentWeighting { get; set; }

    private readonly List<int> _applicableGrades = new();
    private readonly List<ISubTopic> _subTopics = new();
    private readonly List<IContentItem> _contentItems = new();
    private readonly List<ILearningObjective> _learningObjectives = new();
    private readonly List<ITopicProgression> _topicProgressions = new();
    private readonly List<Guid> _linkedTopicIds = new();

    public IReadOnlyCollection<int> ApplicableGrades => _applicableGrades.AsReadOnly();
    public IReadOnlyCollection<ISubTopic> SubTopics => _subTopics.AsReadOnly();
    public IReadOnlyCollection<IContentItem> ContentItems => _contentItems.AsReadOnly();
    public IReadOnlyCollection<ILearningObjective> LearningObjectives => _learningObjectives.AsReadOnly();
    public IReadOnlyCollection<ITopicProgression> TopicProgressions => _topicProgressions.AsReadOnly();
    public IReadOnlyCollection<Guid>? LinkedTopicIds => _linkedTopicIds.Count > 0 ? _linkedTopicIds.AsReadOnly() : null;

    public TopicEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddApplicableGrade(int grade) => _applicableGrades.Add(grade);
    public void AddSubTopic(ISubTopic subTopic) => _subTopics.Add(subTopic);
    public void AddContentItem(IContentItem contentItem) => _contentItems.Add(contentItem);
    public void AddLearningObjective(ILearningObjective objective) => _learningObjectives.Add(objective);
    public void AddTopicProgression(ITopicProgression progression) => _topicProgressions.Add(progression);
    public void AddLinkedTopic(Guid topicId) => _linkedTopicIds.Add(topicId);
}

/// <summary>
/// Entity implementation of ISubTopic.
/// </summary>
public class SubTopicEntity : ISubTopic
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid TopicId { get; set; }

    private readonly List<IContentItem> _contentItems = new();
    private readonly List<ILearningObjective> _learningObjectives = new();

    public IReadOnlyCollection<IContentItem> ContentItems => _contentItems.AsReadOnly();
    public IReadOnlyCollection<ILearningObjective> LearningObjectives => _learningObjectives.AsReadOnly();

    public SubTopicEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddContentItem(IContentItem contentItem) => _contentItems.Add(contentItem);
    public void AddLearningObjective(ILearningObjective objective) => _learningObjectives.Add(objective);
}

/// <summary>
/// Entity implementation of IContentItem.
/// </summary>
public class ContentItemEntity : IContentItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Sequence { get; set; }
    public int? Grade { get; set; }

    public ContentItemEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of ILearningObjective.
/// </summary>
public class LearningObjectiveEntity : ILearningObjective
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public CognitiveLevel CognitiveLevel { get; set; }

    private readonly List<Guid> _skillIds = new();
    public IReadOnlyCollection<Guid>? SkillIds => _skillIds.Count > 0 ? _skillIds.AsReadOnly() : null;

    public LearningObjectiveEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddSkill(Guid skillId) => _skillIds.Add(skillId);
}

/// <summary>
/// Entity implementation of ITopicProgression.
/// </summary>
public class TopicProgressionEntity : ITopicProgression
{
    public int Grade { get; set; }
    public string Description { get; set; } = string.Empty;
    public CognitiveLevel ExpectedLevel { get; set; }
}

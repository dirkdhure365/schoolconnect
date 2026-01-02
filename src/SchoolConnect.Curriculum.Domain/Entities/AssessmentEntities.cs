using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Entity implementation of IAssessmentPolicy.
/// </summary>
public class AssessmentPolicyEntity : IAssessmentPolicy
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }
    public string? Description { get; set; }
    public IProgrammeOfAssessment? ProgrammeOfAssessment { get; set; }

    private readonly List<IAssessmentComponent> _components = new();
    private readonly List<IAchievementScale> _achievementScales = new();

    public IReadOnlyCollection<IAssessmentComponent> Components => _components.AsReadOnly();
    public IReadOnlyCollection<IAchievementScale> AchievementScales => _achievementScales.AsReadOnly();

    public AssessmentPolicyEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddComponent(IAssessmentComponent component) => _components.Add(component);
    public void AddAchievementScale(IAchievementScale scale) => _achievementScales.Add(scale);
}

/// <summary>
/// Entity implementation of IGradeAssessmentRequirements.
/// </summary>
public class GradeAssessmentRequirementsEntity : IGradeAssessmentRequirements
{
    public Guid Id { get; set; }
    public int Grade { get; set; }
    public int MinimumFormalAssessments { get; set; }
    public decimal SchoolBasedAssessmentWeight { get; set; }
    public decimal FinalExaminationWeight { get; set; }

    private readonly List<IFormalAssessmentTask> _requiredTasks = new();
    public IReadOnlyCollection<IFormalAssessmentTask> RequiredTasks => _requiredTasks.AsReadOnly();

    public GradeAssessmentRequirementsEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddRequiredTask(IFormalAssessmentTask task) => _requiredTasks.Add(task);
}

/// <summary>
/// Entity implementation of IAssessmentComponent.
/// </summary>
public class AssessmentComponentEntity : IAssessmentComponent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AssessmentType Type { get; set; }
    public decimal Weight { get; set; }
    public int? DurationMinutes { get; set; }
    public int TotalMarks { get; set; }
    public string? Description { get; set; }

    public AssessmentComponentEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IFormalAssessmentTask.
/// </summary>
public class FormalAssessmentTaskEntity : IFormalAssessmentTask
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AssessmentType Type { get; set; }
    public SchoolTerm Term { get; set; }
    public int TotalMarks { get; set; }
    public decimal? Weight { get; set; }
    public int? DurationMinutes { get; set; }

    private readonly List<Guid> _topicIds = new();
    public IReadOnlyCollection<Guid> TopicIds => _topicIds.AsReadOnly();

    public FormalAssessmentTaskEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddTopic(Guid topicId) => _topicIds.Add(topicId);
}

/// <summary>
/// Entity implementation of IProgrammeOfAssessment.
/// </summary>
public class ProgrammeOfAssessmentEntity : IProgrammeOfAssessment
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }
    public int Grade { get; set; }
    public string? Description { get; set; }

    private readonly List<IFormalAssessmentTask> _formalTasks = new();
    public IReadOnlyCollection<IFormalAssessmentTask> FormalTasks => _formalTasks.AsReadOnly();

    public ProgrammeOfAssessmentEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddFormalTask(IFormalAssessmentTask task) => _formalTasks.Add(task);
}

/// <summary>
/// Entity implementation of IAchievementScale.
/// </summary>
public class AchievementScaleEntity : IAchievementScale
{
    public string Rating { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MinPercentage { get; set; }
    public decimal MaxPercentage { get; set; }
}

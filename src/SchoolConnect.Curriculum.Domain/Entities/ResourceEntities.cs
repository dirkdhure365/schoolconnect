using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Entity implementation of IResource.
/// </summary>
public class ResourceEntity : IResource
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ResourceCategory Category { get; set; }
    public string? Description { get; set; }
    public bool IsRequired { get; set; }
    public string? Provider { get; set; }
    public string? Url { get; set; }
    public string? Isbn { get; set; }

    public ResourceEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IProject.
/// </summary>
public class ProjectEntity : IProject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid SubjectId { get; set; }
    public int Grade { get; set; }
    public SchoolTerm Term { get; set; }
    public int TotalMarks { get; set; }
    public decimal Weight { get; set; }

    private readonly List<IProjectPhase> _phases = new();
    private readonly List<IProjectCriterion> _criteria = new();
    private readonly List<Guid> _topicIds = new();

    public IReadOnlyCollection<IProjectPhase> Phases => _phases.AsReadOnly();
    public IReadOnlyCollection<IProjectCriterion> Criteria => _criteria.AsReadOnly();
    public IReadOnlyCollection<Guid> TopicIds => _topicIds.AsReadOnly();

    public ProjectEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddPhase(IProjectPhase phase) => _phases.Add(phase);
    public void AddCriterion(IProjectCriterion criterion) => _criteria.Add(criterion);
    public void AddTopic(Guid topicId) => _topicIds.Add(topicId);
}

/// <summary>
/// Entity implementation of IProjectPhase.
/// </summary>
public class ProjectPhaseEntity : IProjectPhase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Sequence { get; set; }
    public int? DurationWeeks { get; set; }

    public ProjectPhaseEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IProjectCriterion.
/// </summary>
public class ProjectCriterionEntity : IProjectCriterion
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Marks { get; set; }
    public CognitiveLevel? CognitiveLevel { get; set; }

    public ProjectCriterionEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IPracticalAssessment.
/// </summary>
public class PracticalAssessmentEntity : IPracticalAssessment
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid SubjectId { get; set; }
    public int Grade { get; set; }
    public int TotalMarks { get; set; }
    public decimal Weight { get; set; }

    private readonly List<IPracticalPhase> _phases = new();
    private readonly List<IProgrammingComponent> _programmingComponents = new();
    private readonly List<IPracticalCriterion> _criteria = new();
    private readonly List<Guid> _topicIds = new();

    public IReadOnlyCollection<IPracticalPhase> Phases => _phases.AsReadOnly();
    public IReadOnlyCollection<IProgrammingComponent> ProgrammingComponents => _programmingComponents.AsReadOnly();
    public IReadOnlyCollection<IPracticalCriterion> Criteria => _criteria.AsReadOnly();
    public IReadOnlyCollection<Guid> TopicIds => _topicIds.AsReadOnly();

    public PracticalAssessmentEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddPhase(IPracticalPhase phase) => _phases.Add(phase);
    public void AddProgrammingComponent(IProgrammingComponent component) => _programmingComponents.Add(component);
    public void AddCriterion(IPracticalCriterion criterion) => _criteria.Add(criterion);
    public void AddTopic(Guid topicId) => _topicIds.Add(topicId);
}

/// <summary>
/// Entity implementation of IPracticalPhase.
/// </summary>
public class PracticalPhaseEntity : IPracticalPhase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public SchoolTerm Term { get; set; }
    public int Marks { get; set; }
    public int Sequence { get; set; }

    public PracticalPhaseEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IProgrammingComponent.
/// </summary>
public class ProgrammingComponentEntity : IProgrammingComponent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ComponentType Type { get; set; }
    public string? Description { get; set; }
    public bool IsMandatory { get; set; }
    public string? Examples { get; set; }

    public ProgrammingComponentEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IPracticalCriterion.
/// </summary>
public class PracticalCriterionEntity : IPracticalCriterion
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Marks { get; set; }
    public CognitiveLevel? CognitiveLevel { get; set; }

    public PracticalCriterionEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IGlossary.
/// </summary>
public class GlossaryEntity : IGlossary
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }

    private readonly List<IGlossaryTerm> _terms = new();
    public IReadOnlyCollection<IGlossaryTerm> Terms => _terms.AsReadOnly();

    public GlossaryEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddTerm(IGlossaryTerm term) => _terms.Add(term);
}

/// <summary>
/// Entity implementation of IGlossaryTerm.
/// </summary>
public class GlossaryTermEntity : IGlossaryTerm
{
    public Guid Id { get; set; }
    public string Term { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string? Context { get; set; }

    private readonly List<string> _relatedTerms = new();
    public IReadOnlyCollection<string>? RelatedTerms => _relatedTerms.Count > 0 ? _relatedTerms.AsReadOnly() : null;

    public GlossaryTermEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddRelatedTerm(string term) => _relatedTerms.Add(term);
}

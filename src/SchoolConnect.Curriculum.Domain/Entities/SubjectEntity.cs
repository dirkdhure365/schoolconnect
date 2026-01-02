using SchoolConnect.Curriculum.Domain.Abstractions;
using SchoolConnect.Curriculum.Domain.Enums;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Entity implementation of ISubject.
/// </summary>
public class SubjectEntity : ISubject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public SubjectType Type { get; set; }
    public bool IsCompulsory { get; set; }
    public string? Description { get; set; }

    private readonly List<Guid> _applicablePhaseIds = new();
    private readonly List<int> _applicableGrades = new();
    private readonly List<string> _aims = new();
    private readonly List<ISkill> _skills = new();
    private readonly List<IConcept> _concepts = new();
    private readonly List<ITopic> _topics = new();
    private readonly List<IResource> _resources = new();
    private readonly List<ISubject> _components = new();

    public IReadOnlyCollection<Guid> ApplicablePhaseIds => _applicablePhaseIds.AsReadOnly();
    public IReadOnlyCollection<int> ApplicableGrades => _applicableGrades.AsReadOnly();
    public IReadOnlyCollection<string> Aims => _aims.AsReadOnly();
    public IReadOnlyCollection<ISkill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<IConcept> Concepts => _concepts.AsReadOnly();
    public IReadOnlyCollection<ITopic> Topics => _topics.AsReadOnly();
    public IReadOnlyCollection<IResource> Resources => _resources.AsReadOnly();
    public IAssessmentPolicy? AssessmentPolicy { get; set; }
    public IReadOnlyCollection<ISubject>? Components => _components.Count > 0 ? _components.AsReadOnly() : null;
    public IGlossary? Glossary { get; set; }

    public SubjectEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddApplicablePhase(Guid phaseId) => _applicablePhaseIds.Add(phaseId);
    public void AddApplicableGrade(int grade) => _applicableGrades.Add(grade);
    public void AddAim(string aim) => _aims.Add(aim);
    public void AddSkill(ISkill skill) => _skills.Add(skill);
    public void AddConcept(IConcept concept) => _concepts.Add(concept);
    public void AddTopic(ITopic topic) => _topics.Add(topic);
    public void AddResource(IResource resource) => _resources.Add(resource);
    public void AddComponent(ISubject component) => _components.Add(component);
}

/// <summary>
/// Entity implementation of ISkill.
/// </summary>
public class SkillEntity : ISkill
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public SkillCategory Category { get; set; }

    public SkillEntity()
    {
        Id = Guid.NewGuid();
    }
}

/// <summary>
/// Entity implementation of IConcept.
/// </summary>
public class ConceptEntity : IConcept
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    private readonly List<Guid> _relatedConceptIds = new();
    public IReadOnlyCollection<Guid>? RelatedConceptIds => _relatedConceptIds.Count > 0 ? _relatedConceptIds.AsReadOnly() : null;

    public ConceptEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddRelatedConcept(Guid conceptId) => _relatedConceptIds.Add(conceptId);
}

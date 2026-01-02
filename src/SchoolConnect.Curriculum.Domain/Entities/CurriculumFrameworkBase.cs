using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Base implementation of ICurriculumFramework.
/// </summary>
public abstract class CurriculumFrameworkBase : ICurriculumFramework
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ExaminationBoard { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public string? Description { get; set; }

    private readonly List<IEducationalPhase> _phases = new();
    private readonly List<ISubject> _subjects = new();
    private readonly List<string> _principles = new();
    private readonly List<string> _generalAims = new();

    public IReadOnlyCollection<IEducationalPhase> Phases => _phases.AsReadOnly();
    public IReadOnlyCollection<ISubject> Subjects => _subjects.AsReadOnly();
    public IReadOnlyCollection<string> Principles => _principles.AsReadOnly();
    public IReadOnlyCollection<string> GeneralAims => _generalAims.AsReadOnly();

    protected CurriculumFrameworkBase()
    {
        Id = Guid.NewGuid();
    }

    public void AddPhase(IEducationalPhase phase) => _phases.Add(phase);
    public void AddSubject(ISubject subject) => _subjects.Add(subject);
    public void AddPrinciple(string principle) => _principles.Add(principle);
    public void AddGeneralAim(string aim) => _generalAims.Add(aim);

    public IEnumerable<ISubject> GetSubjectsByPhase(Guid phaseId)
    {
        var phase = _phases.FirstOrDefault(p => p.Id == phaseId);
        if (phase == null)
            return Enumerable.Empty<ISubject>();

        return _subjects.Where(s => s.ApplicablePhaseIds.Contains(phaseId));
    }

    public IEnumerable<ISubject> GetSubjectsByGrade(int grade)
    {
        return _subjects.Where(s => s.ApplicableGrades.Contains(grade));
    }
}

using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Domain.Entities;

/// <summary>
/// Entity implementation of IEducationalPhase.
/// </summary>
public class EducationalPhaseEntity : IEducationalPhase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int StartGrade { get; set; }
    public int EndGrade { get; set; }
    public int MinimumTeachingHours { get; set; }
    public int MaximumTeachingHours { get; set; }

    private readonly List<IGradeCurriculum> _gradeCurricula = new();
    public IReadOnlyCollection<IGradeCurriculum> GradeCurricula => _gradeCurricula.AsReadOnly();

    public EducationalPhaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public void AddGradeCurriculum(IGradeCurriculum gradeCurriculum) => _gradeCurricula.Add(gradeCurriculum);

    public bool ContainsGrade(int grade)
    {
        return grade >= StartGrade && grade <= EndGrade;
    }
}

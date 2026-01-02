using SchoolConnect.Curriculum.Domain.Abstractions;

namespace SchoolConnect.Curriculum.Application.Services;

/// <summary>
/// Extended curriculum service interface for practical assessments.
/// </summary>
public interface IPracticalCurriculumService : ICurriculumService
{
    /// <summary>
    /// Gets practical assessment (PAT) for a subject and grade.
    /// </summary>
    Task<IPracticalAssessment?> GetPracticalAssessmentAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets projects for a subject and grade.
    /// </summary>
    Task<IEnumerable<IProject>> GetProjectsAsync(Guid subjectId, int grade);

    /// <summary>
    /// Gets programming components required for a subject.
    /// </summary>
    Task<IEnumerable<IProgrammingComponent>> GetProgrammingComponentsAsync(Guid subjectId, int grade);
}

namespace SchoolConnect.EducationSystem.Application.Interfaces;

/// <summary>
/// Core curriculum service interface for examination boards
/// </summary>
public interface ICurriculumService
{
    string ServiceCode { get; }
    string BoardName { get; }
    string Country { get; }
    Task<bool> ValidateGradeAsync(string grade, string programLevel);
    Task<string> GetGradeDescriptionAsync(string grade, string programLevel);
}

/// <summary>
/// Extended curriculum service interface for boards that support practical assessments
/// </summary>
public interface IPracticalCurriculumService : ICurriculumService
{
    Task<bool> SupportsPracticalAssessmentAsync(string subjectCode);
    Task<List<string>> GetPracticalAssessmentRequirementsAsync(string subjectCode);
}

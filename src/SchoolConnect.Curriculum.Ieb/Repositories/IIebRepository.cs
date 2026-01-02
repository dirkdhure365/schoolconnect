namespace SchoolConnect.Curriculum.Ieb.Repositories;

/// <summary>
/// Repository interface for IEB-specific data access
/// </summary>
public interface IIebRepository
{
    Task<IebGrade?> GetGradeByLevelAsync(int level);
    Task<IebGrade?> GetGradeByPercentageAsync(int percentage);
    Task<List<IebGrade>> GetAllGradesAsync();
    Task<IebProgram?> GetProgramByNameAsync(string programName);
    Task<List<IebProgram>> GetAllProgramsAsync();
    Task<bool> IsPracticalAssessmentRequiredAsync(string subjectCode);
}

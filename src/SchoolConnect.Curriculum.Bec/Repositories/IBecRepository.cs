namespace SchoolConnect.Curriculum.Bec.Repositories;

/// <summary>
/// Repository interface for BEC-specific data access
/// </summary>
public interface IBecRepository
{
    Task<BgcseGrade?> GetBgcseGradeByPercentageAsync(int percentage);
    Task<BecJcGrade?> GetJcGradeByPercentageAsync(int percentage);
    Task<List<BgcseGrade>> GetAllBgcseGradesAsync();
    Task<List<BecJcGrade>> GetAllJcGradesAsync();
    Task<BecProgram?> GetProgramByNameAsync(string programName);
    Task<List<BecProgram>> GetAllProgramsAsync();
}

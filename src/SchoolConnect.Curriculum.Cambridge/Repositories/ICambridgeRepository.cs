namespace SchoolConnect.Curriculum.Cambridge.Repositories;

/// <summary>
/// Repository interface for Cambridge-specific data access
/// </summary>
public interface ICambridgeRepository
{
    Task<CambridgeGrade?> GetGradeByPercentageAsync(int percentage, string programLevel);
    Task<List<CambridgeGrade>> GetAllGradesAsync(string programLevel);
    Task<CambridgeProgram?> GetProgramByNameAsync(string programName);
    Task<List<CambridgeProgram>> GetAllProgramsAsync();
}

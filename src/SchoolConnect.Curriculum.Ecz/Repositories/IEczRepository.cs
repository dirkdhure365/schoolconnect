namespace SchoolConnect.Curriculum.Ecz.Repositories;

/// <summary>
/// Repository interface for ECZ-specific data access
/// </summary>
public interface IEczRepository
{
    Task<EczGrade?> GetSchoolCertificateGradeByNumberAsync(int gradeNumber);
    Task<EczGrade?> GetSchoolCertificateGradeByPercentageAsync(int percentage);
    Task<List<EczGrade>> GetAllSchoolCertificateGradesAsync();
    Task<EczJuniorGrade?> GetGrade9GradeByPercentageAsync(int percentage);
    Task<List<EczJuniorGrade>> GetAllGrade9GradesAsync();
    Task<EczDivision?> GetDivisionByPointsAsync(int totalPoints);
    Task<List<EczDivision>> GetAllDivisionsAsync();
    Task<EczProgram?> GetProgramByNameAsync(string programName);
    Task<List<EczProgram>> GetAllProgramsAsync();
}

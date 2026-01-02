using SchoolConnect.Curriculum.Ecz;
using SchoolConnect.Curriculum.Ecz.Repositories;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class EczMongoRepository : IEczRepository
{
    private readonly EczFramework _framework;

    public EczMongoRepository()
    {
        _framework = new EczFramework();
    }

    public Task<EczGrade?> GetSchoolCertificateGradeByNumberAsync(int gradeNumber)
    {
        var grade = _framework.SchoolCertificateGrades.FirstOrDefault(g => g.Grade == gradeNumber);
        return Task.FromResult(grade);
    }

    public Task<EczGrade?> GetSchoolCertificateGradeByPercentageAsync(int percentage)
    {
        var grade = _framework.SchoolCertificateGrades.FirstOrDefault(g => 
            percentage >= g.MinPercentage && percentage <= g.MaxPercentage);
        return Task.FromResult(grade);
    }

    public Task<List<EczGrade>> GetAllSchoolCertificateGradesAsync()
    {
        return Task.FromResult(_framework.SchoolCertificateGrades);
    }

    public Task<EczJuniorGrade?> GetGrade9GradeByPercentageAsync(int percentage)
    {
        var grade = _framework.Grade9Grades.FirstOrDefault(g => 
            percentage >= g.MinPercentage && percentage <= g.MaxPercentage);
        return Task.FromResult(grade);
    }

    public Task<List<EczJuniorGrade>> GetAllGrade9GradesAsync()
    {
        return Task.FromResult(_framework.Grade9Grades);
    }

    public Task<EczDivision?> GetDivisionByPointsAsync(int totalPoints)
    {
        var division = _framework.SchoolCertificateDivisions.FirstOrDefault(d => 
            totalPoints >= d.MinPoints && totalPoints <= d.MaxPoints);
        return Task.FromResult(division);
    }

    public Task<List<EczDivision>> GetAllDivisionsAsync()
    {
        return Task.FromResult(_framework.SchoolCertificateDivisions);
    }

    public Task<EczProgram?> GetProgramByNameAsync(string programName)
    {
        var program = _framework.Programs.FirstOrDefault(p => 
            p.Name.Equals(programName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(program);
    }

    public Task<List<EczProgram>> GetAllProgramsAsync()
    {
        return Task.FromResult(_framework.Programs);
    }
}

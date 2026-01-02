using SchoolConnect.Curriculum.Cambridge;
using SchoolConnect.Curriculum.Cambridge.Repositories;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class CambridgeMongoRepository : ICambridgeRepository
{
    private readonly CambridgeFramework _framework;

    public CambridgeMongoRepository()
    {
        _framework = new CambridgeFramework();
    }

    public Task<CambridgeGrade?> GetGradeByPercentageAsync(int percentage, string programLevel)
    {
        var grades = programLevel.ToUpper() switch
        {
            "IGCSE" or "CAMBRIDGE UPPER SECONDARY" => _framework.IgcseGrades,
            "A LEVEL" or "A-LEVEL" => _framework.ALevelGrades,
            "AS LEVEL" or "AS-LEVEL" => _framework.AsLevelGrades,
            _ => new List<CambridgeGrade>()
        };

        var grade = grades.FirstOrDefault(g => percentage >= g.MinPercentage && percentage <= g.MaxPercentage);
        return Task.FromResult(grade);
    }

    public Task<List<CambridgeGrade>> GetAllGradesAsync(string programLevel)
    {
        var grades = programLevel.ToUpper() switch
        {
            "IGCSE" or "CAMBRIDGE UPPER SECONDARY" => _framework.IgcseGrades,
            "A LEVEL" or "A-LEVEL" => _framework.ALevelGrades,
            "AS LEVEL" or "AS-LEVEL" => _framework.AsLevelGrades,
            _ => new List<CambridgeGrade>()
        };

        return Task.FromResult(grades);
    }

    public Task<CambridgeProgram?> GetProgramByNameAsync(string programName)
    {
        var program = _framework.Programs.FirstOrDefault(p => 
            p.Name.Equals(programName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(program);
    }

    public Task<List<CambridgeProgram>> GetAllProgramsAsync()
    {
        return Task.FromResult(_framework.Programs);
    }
}

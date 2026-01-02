using SchoolConnect.Curriculum.Bec;
using SchoolConnect.Curriculum.Bec.Repositories;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class BecMongoRepository : IBecRepository
{
    private readonly BecFramework _framework;

    public BecMongoRepository()
    {
        _framework = new BecFramework();
    }

    public Task<BgcseGrade?> GetBgcseGradeByPercentageAsync(int percentage)
    {
        var grade = _framework.BgcseGrades.FirstOrDefault(g => 
            percentage >= g.MinPercentage && percentage <= g.MaxPercentage);
        return Task.FromResult(grade);
    }

    public Task<BecJcGrade?> GetJcGradeByPercentageAsync(int percentage)
    {
        var grade = _framework.JcGrades.FirstOrDefault(g => 
            percentage >= g.MinPercentage && percentage <= g.MaxPercentage);
        return Task.FromResult(grade);
    }

    public Task<List<BgcseGrade>> GetAllBgcseGradesAsync()
    {
        return Task.FromResult(_framework.BgcseGrades);
    }

    public Task<List<BecJcGrade>> GetAllJcGradesAsync()
    {
        return Task.FromResult(_framework.JcGrades);
    }

    public Task<BecProgram?> GetProgramByNameAsync(string programName)
    {
        var program = _framework.Programs.FirstOrDefault(p => 
            p.Name.Equals(programName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(program);
    }

    public Task<List<BecProgram>> GetAllProgramsAsync()
    {
        return Task.FromResult(_framework.Programs);
    }
}

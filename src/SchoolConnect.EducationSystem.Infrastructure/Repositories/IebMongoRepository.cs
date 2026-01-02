using SchoolConnect.Curriculum.Ieb;
using SchoolConnect.Curriculum.Ieb.Repositories;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public class IebMongoRepository : IIebRepository
{
    private readonly IebFramework _framework;

    public IebMongoRepository()
    {
        _framework = new IebFramework();
    }

    public Task<IebGrade?> GetGradeByLevelAsync(int level)
    {
        var grade = _framework.NscGrades.FirstOrDefault(g => g.Level == level);
        return Task.FromResult(grade);
    }

    public Task<IebGrade?> GetGradeByPercentageAsync(int percentage)
    {
        var grade = _framework.NscGrades.FirstOrDefault(g => 
            percentage >= g.MinPercentage && percentage <= g.MaxPercentage);
        return Task.FromResult(grade);
    }

    public Task<List<IebGrade>> GetAllGradesAsync()
    {
        return Task.FromResult(_framework.NscGrades);
    }

    public Task<IebProgram?> GetProgramByNameAsync(string programName)
    {
        var program = _framework.Programs.FirstOrDefault(p => 
            p.Name.Equals(programName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(program);
    }

    public Task<List<IebProgram>> GetAllProgramsAsync()
    {
        return Task.FromResult(_framework.Programs);
    }

    public Task<bool> IsPracticalAssessmentRequiredAsync(string subjectCode)
    {
        var required = _framework.PracticalAssessmentSubjects.Any(s => 
            subjectCode.ToUpper().Contains(s.ToUpper().Replace(" ", "")));
        return Task.FromResult(required);
    }
}

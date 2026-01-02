using SchoolConnect.EducationSystem.Application.Interfaces;

namespace SchoolConnect.Curriculum.Bec.Services;

public class BecCurriculumService : ICurriculumService
{
    private readonly BecFramework _framework;

    public BecCurriculumService()
    {
        _framework = new BecFramework();
    }

    public string ServiceCode => _framework.ServiceCode;
    public string BoardName => _framework.ExaminationBoard;
    public string Country => _framework.Country;

    public Task<bool> ValidateGradeAsync(string grade, string programLevel)
    {
        var grades = programLevel.ToUpper() switch
        {
            "JC" or "JUNIOR CERTIFICATE" or "JUNIOR SECONDARY" => _framework.JcGrades.Select(g => g.Grade).ToList(),
            "BGCSE" or "SENIOR SECONDARY" => _framework.BgcseGrades.Select(g => g.Grade).ToList(),
            _ => new List<string>()
        };

        var isValid = grades.Any(g => g.Equals(grade, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(isValid);
    }

    public Task<string> GetGradeDescriptionAsync(string grade, string programLevel)
    {
        if (programLevel.ToUpper().Contains("JC") || 
            programLevel.ToUpper().Contains("JUNIOR"))
        {
            var gradeInfo = _framework.JcGrades.FirstOrDefault(g => 
                g.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase));
            if (gradeInfo != null)
            {
                return Task.FromResult($"{gradeInfo.Description} ({gradeInfo.MinPercentage}-{gradeInfo.MaxPercentage}%, Points: {gradeInfo.Points})");
            }
        }
        else if (programLevel.ToUpper().Contains("BGCSE") || 
                 programLevel.ToUpper().Contains("SENIOR"))
        {
            var gradeInfo = _framework.BgcseGrades.FirstOrDefault(g => 
                g.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase));
            if (gradeInfo != null)
            {
                return Task.FromResult($"{gradeInfo.Description} ({gradeInfo.MinPercentage}-{gradeInfo.MaxPercentage}%, Points: {gradeInfo.Points})");
            }
        }

        return Task.FromResult("Unknown grade");
    }
}

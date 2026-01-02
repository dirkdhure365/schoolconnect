using SchoolConnect.EducationSystem.Application.Interfaces;

namespace SchoolConnect.Curriculum.Ieb.Services;

public class IebCurriculumService : ICurriculumService, IPracticalCurriculumService
{
    private readonly IebFramework _framework;

    public IebCurriculumService()
    {
        _framework = new IebFramework();
    }

    public string ServiceCode => _framework.ServiceCode;
    public string BoardName => _framework.ExaminationBoard;
    public string Country => _framework.Country;

    public Task<bool> ValidateGradeAsync(string grade, string programLevel)
    {
        // IEB uses numeric levels 1-7
        if (int.TryParse(grade, out int level))
        {
            var isValid = _framework.NscGrades.Any(g => g.Level == level);
            return Task.FromResult(isValid);
        }
        
        // Also accept percentage ranges
        if (grade.Contains("%") || grade.Contains("-"))
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public Task<string> GetGradeDescriptionAsync(string grade, string programLevel)
    {
        if (int.TryParse(grade, out int level))
        {
            var gradeInfo = _framework.NscGrades.FirstOrDefault(g => g.Level == level);
            if (gradeInfo != null)
            {
                return Task.FromResult($"Level {gradeInfo.Level}: {gradeInfo.Achievement} ({gradeInfo.MinPercentage}-{gradeInfo.MaxPercentage}%)");
            }
        }

        return Task.FromResult("Unknown grade");
    }

    public Task<bool> SupportsPracticalAssessmentAsync(string subjectCode)
    {
        var supports = _framework.PracticalAssessmentSubjects.Any(s => 
            subjectCode.ToUpper().Contains(s.ToUpper().Replace(" ", "")));
        return Task.FromResult(supports);
    }

    public Task<List<string>> GetPracticalAssessmentRequirementsAsync(string subjectCode)
    {
        var requirements = new List<string>();

        if (subjectCode.ToUpper().Contains("CAT") || 
            subjectCode.ToUpper().Contains("COMPUTER APPLICATIONS"))
        {
            requirements.AddRange(new[]
            {
                "Practical Assessment Task (PAT) - 25% of final mark",
                "PAT must be completed throughout the year",
                "Minimum 30 hours work time",
                "Must demonstrate problem-solving and solution development",
                "Documentation and testing required",
                "Moderated by IEB"
            });
        }
        else if (subjectCode.ToUpper().Contains("IT") || 
                 subjectCode.ToUpper().Contains("INFORMATION TECHNOLOGY"))
        {
            requirements.AddRange(new[]
            {
                "Practical Assessment Task (PAT) - 25% of final mark",
                "PAT completed in Grade 12",
                "Development of practical solution",
                "Database design and implementation",
                "Documentation required",
                "IEB moderation"
            });
        }
        else if (subjectCode.ToUpper().Contains("SCIENCES") || 
                 subjectCode.ToUpper().Contains("SCIENCE"))
        {
            requirements.AddRange(new[]
            {
                "Practical examination component",
                "Laboratory skills assessment",
                "Scientific investigation skills",
                "Data analysis and interpretation",
                "Safety procedures compliance"
            });
        }
        else if (subjectCode.ToUpper().Contains("DESIGN") || 
                 subjectCode.ToUpper().Contains("ARTS"))
        {
            requirements.AddRange(new[]
            {
                "Portfolio of practical work",
                "Research and development process",
                "Final product/performance",
                "Reflection and evaluation"
            });
        }

        return Task.FromResult(requirements);
    }
}

using SchoolConnect.EducationSystem.Application.Interfaces;

namespace SchoolConnect.Curriculum.Cambridge.Services;

public class CambridgeCurriculumService : ICurriculumService, IPracticalCurriculumService
{
    private readonly CambridgeFramework _framework;

    public CambridgeCurriculumService()
    {
        _framework = new CambridgeFramework();
    }

    public string ServiceCode => _framework.ServiceCode;
    public string BoardName => _framework.ExaminationBoard;
    public string Country => _framework.Country;

    public Task<bool> ValidateGradeAsync(string grade, string programLevel)
    {
        var grades = programLevel.ToUpper() switch
        {
            "IGCSE" or "CAMBRIDGE UPPER SECONDARY" => _framework.IgcseGrades,
            "A LEVEL" or "A-LEVEL" => _framework.ALevelGrades,
            "AS LEVEL" or "AS-LEVEL" => _framework.AsLevelGrades,
            _ => new List<CambridgeGrade>()
        };

        var isValid = grades.Any(g => g.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(isValid);
    }

    public Task<string> GetGradeDescriptionAsync(string grade, string programLevel)
    {
        var grades = programLevel.ToUpper() switch
        {
            "IGCSE" or "CAMBRIDGE UPPER SECONDARY" => _framework.IgcseGrades,
            "A LEVEL" or "A-LEVEL" => _framework.ALevelGrades,
            "AS LEVEL" or "AS-LEVEL" => _framework.AsLevelGrades,
            _ => new List<CambridgeGrade>()
        };

        var gradeInfo = grades.FirstOrDefault(g => g.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase));
        if (gradeInfo != null)
        {
            return Task.FromResult($"{gradeInfo.Description} ({gradeInfo.MinPercentage}-{gradeInfo.MaxPercentage}%)");
        }

        return Task.FromResult("Unknown grade");
    }

    public Task<bool> SupportsPracticalAssessmentAsync(string subjectCode)
    {
        // Cambridge supports practical assessments for sciences, computer science, and some other subjects
        var practicalSubjects = new List<string>
        {
            "PHYSICS", "CHEMISTRY", "BIOLOGY", "SCIENCE", "COMBINED SCIENCE",
            "COMPUTER SCIENCE", "ICT", "INFORMATION TECHNOLOGY",
            "DESIGN AND TECHNOLOGY", "FOOD AND NUTRITION", "ART AND DESIGN",
            "MUSIC", "DRAMA", "PHYSICAL EDUCATION"
        };

        var supports = practicalSubjects.Any(s => subjectCode.ToUpper().Contains(s));
        return Task.FromResult(supports);
    }

    public Task<List<string>> GetPracticalAssessmentRequirementsAsync(string subjectCode)
    {
        var requirements = new List<string>();

        if (subjectCode.ToUpper().Contains("PHYSICS") || 
            subjectCode.ToUpper().Contains("CHEMISTRY") || 
            subjectCode.ToUpper().Contains("BIOLOGY") ||
            subjectCode.ToUpper().Contains("SCIENCE"))
        {
            requirements.AddRange(new[]
            {
                "Practical examination (2 hours)",
                "Laboratory skills assessment",
                "Safety procedures knowledge",
                "Data analysis and evaluation",
                "Planning and experimental design"
            });
        }
        else if (subjectCode.ToUpper().Contains("COMPUTER SCIENCE") || 
                 subjectCode.ToUpper().Contains("ICT"))
        {
            requirements.AddRange(new[]
            {
                "Practical programming task",
                "Project work (20 hours minimum)",
                "Documentation and testing",
                "Problem-solving skills"
            });
        }
        else if (subjectCode.ToUpper().Contains("ART") || 
                 subjectCode.ToUpper().Contains("DESIGN"))
        {
            requirements.AddRange(new[]
            {
                "Portfolio of coursework",
                "Practical examination",
                "Personal investigation",
                "Final outcome presentation"
            });
        }

        return Task.FromResult(requirements);
    }
}

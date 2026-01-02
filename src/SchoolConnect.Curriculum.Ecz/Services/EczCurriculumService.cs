using SchoolConnect.EducationSystem.Application.Interfaces;

namespace SchoolConnect.Curriculum.Ecz.Services;

public class EczCurriculumService : ICurriculumService
{
    private readonly EczFramework _framework;

    public EczCurriculumService()
    {
        _framework = new EczFramework();
    }

    public string ServiceCode => _framework.ServiceCode;
    public string BoardName => _framework.ExaminationBoard;
    public string Country => _framework.Country;

    public Task<bool> ValidateGradeAsync(string grade, string programLevel)
    {
        if (programLevel.ToUpper().Contains("GRADE 9") || 
            programLevel.ToUpper().Contains("JUNIOR SECONDARY"))
        {
            var isValid = _framework.Grade9Grades.Any(g => 
                g.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(isValid);
        }
        else if (programLevel.ToUpper().Contains("GRADE 12") || 
                 programLevel.ToUpper().Contains("SCHOOL CERTIFICATE") ||
                 programLevel.ToUpper().Contains("SENIOR SECONDARY"))
        {
            // Accept numeric grades 1-9
            if (int.TryParse(grade, out int gradeNum))
            {
                var isValid = _framework.SchoolCertificateGrades.Any(g => g.Grade == gradeNum);
                return Task.FromResult(isValid);
            }
        }

        return Task.FromResult(false);
    }

    public Task<string> GetGradeDescriptionAsync(string grade, string programLevel)
    {
        if (programLevel.ToUpper().Contains("GRADE 9") || 
            programLevel.ToUpper().Contains("JUNIOR SECONDARY"))
        {
            var gradeInfo = _framework.Grade9Grades.FirstOrDefault(g => 
                g.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase));
            if (gradeInfo != null)
            {
                return Task.FromResult($"{gradeInfo.Description} ({gradeInfo.MinPercentage}-{gradeInfo.MaxPercentage}%)");
            }
        }
        else if (programLevel.ToUpper().Contains("GRADE 12") || 
                 programLevel.ToUpper().Contains("SCHOOL CERTIFICATE") ||
                 programLevel.ToUpper().Contains("SENIOR SECONDARY"))
        {
            if (int.TryParse(grade, out int gradeNum))
            {
                var gradeInfo = _framework.SchoolCertificateGrades.FirstOrDefault(g => g.Grade == gradeNum);
                if (gradeInfo != null)
                {
                    return Task.FromResult($"Grade {gradeInfo.Grade}: {gradeInfo.Description} ({gradeInfo.MinPercentage}-{gradeInfo.MaxPercentage}%, Points: {gradeInfo.Points})");
                }
            }
        }

        return Task.FromResult("Unknown grade");
    }

    public Task<string> GetDivisionDescriptionAsync(int totalPoints)
    {
        var division = _framework.SchoolCertificateDivisions.FirstOrDefault(d => 
            totalPoints >= d.MinPoints && totalPoints <= d.MaxPoints);
        
        if (division != null)
        {
            return Task.FromResult($"Division {division.Division}: {division.Description} ({division.MinPoints}-{division.MaxPoints} points)");
        }

        return Task.FromResult(totalPoints < 7 ? "Below Division 1" : "No division achieved (>35 points)");
    }
}

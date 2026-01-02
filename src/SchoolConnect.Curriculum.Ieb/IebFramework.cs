namespace SchoolConnect.Curriculum.Ieb;

/// <summary>
/// Independent Examinations Board (IEB) Framework - South Africa
/// </summary>
public class IebFramework
{
    public string Country => "South Africa";
    public string ExaminationBoard => "Independent Examinations Board";
    public string ServiceCode => "IEB";

    public List<IebProgram> Programs => new()
    {
        new IebProgram 
        { 
            Name = "IEB Primary", 
            Description = "Grade 1-7",
            Grades = new List<string> { "Grade 1", "Grade 2", "Grade 3", "Grade 4", "Grade 5", "Grade 6", "Grade 7" }
        },
        new IebProgram 
        { 
            Name = "IEB Secondary", 
            Description = "Grade 8-9",
            Grades = new List<string> { "Grade 8", "Grade 9" }
        },
        new IebProgram 
        { 
            Name = "IEB NSC", 
            Description = "Grade 10-12 - National Senior Certificate via IEB",
            Grades = new List<string> { "Grade 10", "Grade 11", "Grade 12" }
        }
    };

    public List<string> KeyFeatures => new()
    {
        "Independent schools curriculum",
        "Same phases as CAPS but with IEB-specific assessments",
        "Research-based teaching approach",
        "Extended curriculum options",
        "CAT (Computer Applications Technology) with PAT support",
        "IT (Information Technology) with PAT support",
        "Higher standards and rigorous assessment",
        "Critical thinking emphasis"
    };

    public List<string> Principles => new()
    {
        "Academic excellence",
        "Critical thinking",
        "Holistic education",
        "Research-based learning",
        "Independent thought",
        "Ethical leadership"
    };

    // IEB uses the same 7-point grading scale as CAPS (aligned with DBE)
    public List<IebGrade> NscGrades => new()
    {
        new IebGrade { Level = 7, Description = "Outstanding", MinPercentage = 80, MaxPercentage = 100, Achievement = "Outstanding Achievement" },
        new IebGrade { Level = 6, Description = "Meritorious", MinPercentage = 70, MaxPercentage = 79, Achievement = "Meritorious Achievement" },
        new IebGrade { Level = 5, Description = "Substantial", MinPercentage = 60, MaxPercentage = 69, Achievement = "Substantial Achievement" },
        new IebGrade { Level = 4, Description = "Adequate", MinPercentage = 50, MaxPercentage = 59, Achievement = "Adequate Achievement" },
        new IebGrade { Level = 3, Description = "Moderate", MinPercentage = 40, MaxPercentage = 49, Achievement = "Moderate Achievement" },
        new IebGrade { Level = 2, Description = "Elementary", MinPercentage = 30, MaxPercentage = 39, Achievement = "Elementary Achievement" },
        new IebGrade { Level = 1, Description = "Not Achieved", MinPercentage = 0, MaxPercentage = 29, Achievement = "Not Achieved" }
    };

    public List<string> PracticalAssessmentSubjects => new()
    {
        "Computer Applications Technology (CAT)",
        "Information Technology (IT)",
        "Physical Sciences",
        "Life Sciences",
        "Agricultural Sciences",
        "Engineering Graphics and Design",
        "Design",
        "Visual Arts",
        "Music",
        "Dramatic Arts"
    };
}

public class IebProgram
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Grades { get; set; } = new();
}

public class IebGrade
{
    public int Level { get; set; }
    public string Description { get; set; } = string.Empty;
    public int MinPercentage { get; set; }
    public int MaxPercentage { get; set; }
    public string Achievement { get; set; } = string.Empty;
}

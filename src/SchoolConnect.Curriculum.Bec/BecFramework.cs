namespace SchoolConnect.Curriculum.Bec;

/// <summary>
/// Botswana Examinations Council (BEC) Framework
/// </summary>
public class BecFramework
{
    public string Country => "Botswana";
    public string ExaminationBoard => "Botswana Examinations Council";
    public string ServiceCode => "BEC";

    public List<BecProgram> Programs => new()
    {
        new BecProgram 
        { 
            Name = "Primary Education", 
            Description = "Standard 1-7",
            Standards = new List<string> { "Standard 1", "Standard 2", "Standard 3", "Standard 4", "Standard 5", "Standard 6", "Standard 7" }
        },
        new BecProgram 
        { 
            Name = "Junior Secondary", 
            Description = "Form 1-3 - Junior Certificate (JC)",
            Forms = new List<string> { "Form 1", "Form 2", "Form 3" },
            Qualification = "Junior Certificate (JC)"
        },
        new BecProgram 
        { 
            Name = "Senior Secondary", 
            Description = "Form 4-5 - Botswana General Certificate of Secondary Education (BGCSE)",
            Forms = new List<string> { "Form 4", "Form 5" },
            Qualification = "BGCSE"
        }
    };

    public List<string> KeyFeatures => new()
    {
        "Botswana-specific curriculum content",
        "Focus on Setswana language and culture",
        "Practical and vocational subjects",
        "Continuous assessment component",
        "Emphasis on local context",
        "Aligned with national development goals"
    };

    public List<string> Principles => new()
    {
        "Botho (humanity/respect)",
        "Self-reliance",
        "Unity",
        "Democracy",
        "Development",
        "Cultural preservation"
    };

    // Junior Certificate Grading (A-E scale)
    public List<BecJcGrade> JcGrades => new()
    {
        new BecJcGrade { Grade = "A", Description = "Excellent", MinPercentage = 80, MaxPercentage = 100, Points = 1 },
        new BecJcGrade { Grade = "B", Description = "Very Good", MinPercentage = 70, MaxPercentage = 79, Points = 2 },
        new BecJcGrade { Grade = "C", Description = "Good", MinPercentage = 60, MaxPercentage = 69, Points = 3 },
        new BecJcGrade { Grade = "D", Description = "Satisfactory", MinPercentage = 50, MaxPercentage = 59, Points = 4 },
        new BecJcGrade { Grade = "E", Description = "Pass", MinPercentage = 40, MaxPercentage = 49, Points = 5 },
        new BecJcGrade { Grade = "U", Description = "Ungraded", MinPercentage = 0, MaxPercentage = 39, Points = 9 }
    };

    // BGCSE Grading (A*-G scale, similar to Cambridge)
    public List<BgcseGrade> BgcseGrades => new()
    {
        new BgcseGrade { Grade = "A*", Description = "Outstanding", MinPercentage = 90, MaxPercentage = 100, Points = 1 },
        new BgcseGrade { Grade = "A", Description = "Excellent", MinPercentage = 80, MaxPercentage = 89, Points = 2 },
        new BgcseGrade { Grade = "B", Description = "Very Good", MinPercentage = 70, MaxPercentage = 79, Points = 3 },
        new BgcseGrade { Grade = "C", Description = "Good", MinPercentage = 60, MaxPercentage = 69, Points = 4 },
        new BgcseGrade { Grade = "D", Description = "Satisfactory", MinPercentage = 50, MaxPercentage = 59, Points = 5 },
        new BgcseGrade { Grade = "E", Description = "Pass", MinPercentage = 40, MaxPercentage = 49, Points = 6 },
        new BgcseGrade { Grade = "F", Description = "Weak", MinPercentage = 30, MaxPercentage = 39, Points = 7 },
        new BgcseGrade { Grade = "G", Description = "Poor", MinPercentage = 20, MaxPercentage = 29, Points = 8 },
        new BgcseGrade { Grade = "U", Description = "Ungraded", MinPercentage = 0, MaxPercentage = 19, Points = 9 }
    };
}

public class BecProgram
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Standards { get; set; } = new();
    public List<string> Forms { get; set; } = new();
    public string Qualification { get; set; } = string.Empty;
}

public class BecJcGrade
{
    public string Grade { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPercentage { get; set; }
    public int MaxPercentage { get; set; }
    public int Points { get; set; }
}

public class BgcseGrade
{
    public string Grade { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPercentage { get; set; }
    public int MaxPercentage { get; set; }
    public int Points { get; set; }
}

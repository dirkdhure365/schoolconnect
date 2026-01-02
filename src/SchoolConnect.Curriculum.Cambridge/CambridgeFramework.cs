namespace SchoolConnect.Curriculum.Cambridge;

/// <summary>
/// Cambridge Assessment International Education (CAIE) Framework
/// </summary>
public class CambridgeFramework
{
    public string Country => "Multiple (International)";
    public string ExaminationBoard => "Cambridge Assessment International Education (CAIE)";
    public string ServiceCode => "CAMBRIDGE";
    public string AlternativeCode => "CAIE";

    public List<CambridgeProgram> Programs => new()
    {
        new CambridgeProgram 
        { 
            Name = "Cambridge Primary", 
            Description = "Ages 5-11",
            AgeRange = "5-11",
            Levels = new List<string> { "Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage 5", "Stage 6" }
        },
        new CambridgeProgram 
        { 
            Name = "Cambridge Lower Secondary", 
            Description = "Ages 11-14",
            AgeRange = "11-14",
            Levels = new List<string> { "Stage 7", "Stage 8", "Stage 9" }
        },
        new CambridgeProgram 
        { 
            Name = "Cambridge Upper Secondary: IGCSE", 
            Description = "Ages 14-16",
            AgeRange = "14-16",
            Levels = new List<string> { "Year 10", "Year 11" }
        },
        new CambridgeProgram 
        { 
            Name = "Cambridge Advanced: AS & A Level", 
            Description = "Ages 16-19",
            AgeRange = "16-19",
            Levels = new List<string> { "AS Level", "A Level" }
        }
    };

    public List<string> KeyFeatures => new()
    {
        "International curriculum recognized globally",
        "Modular assessment options",
        "Coursework and practical components",
        "Support for Cambridge Primary Checkpoint, Secondary Checkpoint",
        "Comprehensive subject range",
        "Flexible assessment schedule"
    };

    public List<string> Principles => new()
    {
        "International mindedness",
        "Evidence-based approach",
        "Building skills for life",
        "Critical thinking development",
        "Global perspective"
    };

    public List<CambridgeGrade> IgcseGrades => new()
    {
        new CambridgeGrade { Grade = "A*", Description = "Outstanding", MinPercentage = 90, MaxPercentage = 100, Points = 8 },
        new CambridgeGrade { Grade = "A", Description = "Excellent", MinPercentage = 80, MaxPercentage = 89, Points = 7 },
        new CambridgeGrade { Grade = "B", Description = "Very Good", MinPercentage = 70, MaxPercentage = 79, Points = 6 },
        new CambridgeGrade { Grade = "C", Description = "Good", MinPercentage = 60, MaxPercentage = 69, Points = 5 },
        new CambridgeGrade { Grade = "D", Description = "Satisfactory", MinPercentage = 50, MaxPercentage = 59, Points = 4 },
        new CambridgeGrade { Grade = "E", Description = "Pass", MinPercentage = 40, MaxPercentage = 49, Points = 3 },
        new CambridgeGrade { Grade = "F", Description = "Weak", MinPercentage = 30, MaxPercentage = 39, Points = 2 },
        new CambridgeGrade { Grade = "G", Description = "Poor", MinPercentage = 20, MaxPercentage = 29, Points = 1 },
        new CambridgeGrade { Grade = "U", Description = "Ungraded", MinPercentage = 0, MaxPercentage = 19, Points = 0 }
    };

    public List<CambridgeGrade> ALevelGrades => new()
    {
        new CambridgeGrade { Grade = "A*", Description = "Outstanding", MinPercentage = 90, MaxPercentage = 100, Points = 56, UcasPoints = 56 },
        new CambridgeGrade { Grade = "A", Description = "Excellent", MinPercentage = 80, MaxPercentage = 89, Points = 48, UcasPoints = 48 },
        new CambridgeGrade { Grade = "B", Description = "Very Good", MinPercentage = 70, MaxPercentage = 79, Points = 40, UcasPoints = 40 },
        new CambridgeGrade { Grade = "C", Description = "Good", MinPercentage = 60, MaxPercentage = 69, Points = 32, UcasPoints = 32 },
        new CambridgeGrade { Grade = "D", Description = "Satisfactory", MinPercentage = 50, MaxPercentage = 59, Points = 24, UcasPoints = 24 },
        new CambridgeGrade { Grade = "E", Description = "Pass", MinPercentage = 40, MaxPercentage = 49, Points = 16, UcasPoints = 16 },
        new CambridgeGrade { Grade = "U", Description = "Ungraded", MinPercentage = 0, MaxPercentage = 39, Points = 0, UcasPoints = 0 }
    };

    public List<CambridgeGrade> AsLevelGrades => new()
    {
        new CambridgeGrade { Grade = "A", Description = "Excellent", MinPercentage = 80, MaxPercentage = 100, Points = 20, UcasPoints = 20 },
        new CambridgeGrade { Grade = "B", Description = "Very Good", MinPercentage = 70, MaxPercentage = 79, Points = 16, UcasPoints = 16 },
        new CambridgeGrade { Grade = "C", Description = "Good", MinPercentage = 60, MaxPercentage = 69, Points = 12, UcasPoints = 12 },
        new CambridgeGrade { Grade = "D", Description = "Satisfactory", MinPercentage = 50, MaxPercentage = 59, Points = 10, UcasPoints = 10 },
        new CambridgeGrade { Grade = "E", Description = "Pass", MinPercentage = 40, MaxPercentage = 49, Points = 6, UcasPoints = 6 },
        new CambridgeGrade { Grade = "U", Description = "Ungraded", MinPercentage = 0, MaxPercentage = 39, Points = 0, UcasPoints = 0 }
    };
}

public class CambridgeProgram
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AgeRange { get; set; } = string.Empty;
    public List<string> Levels { get; set; } = new();
}

public class CambridgeGrade
{
    public string Grade { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPercentage { get; set; }
    public int MaxPercentage { get; set; }
    public int Points { get; set; }
    public int UcasPoints { get; set; } // For A Level and AS Level
}

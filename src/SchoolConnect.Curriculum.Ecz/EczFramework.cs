namespace SchoolConnect.Curriculum.Ecz;

/// <summary>
/// Examinations Council of Zambia (ECZ) Framework
/// </summary>
public class EczFramework
{
    public string Country => "Zambia";
    public string ExaminationBoard => "Examinations Council of Zambia";
    public string ServiceCode => "ECZ";

    public List<EczProgram> Programs => new()
    {
        new EczProgram 
        { 
            Name = "Early Childhood Education", 
            Description = "Pre-school",
            Grades = new List<string> { "Pre-school" }
        },
        new EczProgram 
        { 
            Name = "Primary Education", 
            Description = "Grade 1-7",
            Grades = new List<string> { "Grade 1", "Grade 2", "Grade 3", "Grade 4", "Grade 5", "Grade 6", "Grade 7" }
        },
        new EczProgram 
        { 
            Name = "Junior Secondary", 
            Description = "Grade 8-9 - Junior Secondary Leaving Examination",
            Grades = new List<string> { "Grade 8", "Grade 9" },
            Qualification = "Junior Secondary Leaving Examination"
        },
        new EczProgram 
        { 
            Name = "Senior Secondary", 
            Description = "Grade 10-12 - School Certificate / GCE",
            Grades = new List<string> { "Grade 10", "Grade 11", "Grade 12" },
            Qualification = "School Certificate / GCE"
        }
    };

    public List<string> KeyFeatures => new()
    {
        "Zambian national curriculum",
        "Focus on local languages (Bemba, Nyanja, Tonga, Lozi, etc.)",
        "Civic Education as compulsory subject",
        "Practical and technical subjects",
        "Free primary education initiative",
        "Skills development emphasis"
    };

    public List<string> Principles => new()
    {
        "Access for all",
        "Quality education",
        "Relevance to national needs",
        "Equity in education",
        "Partnership with stakeholders",
        "Skills for development"
    };

    // Grade 9 Examination Grading
    public List<EczJuniorGrade> Grade9Grades => new()
    {
        new EczJuniorGrade { Grade = "Merit", Description = "Merit", MinPercentage = 75, MaxPercentage = 100 },
        new EczJuniorGrade { Grade = "Credit", Description = "Credit", MinPercentage = 60, MaxPercentage = 74 },
        new EczJuniorGrade { Grade = "Pass", Description = "Pass", MinPercentage = 40, MaxPercentage = 59 },
        new EczJuniorGrade { Grade = "Fail", Description = "Fail", MinPercentage = 0, MaxPercentage = 39 }
    };

    // Grade 12 Subject Grading (1-9 scale, 1 is highest)
    public List<EczGrade> SchoolCertificateGrades => new()
    {
        new EczGrade { Grade = 1, Description = "Distinction", MinPercentage = 75, MaxPercentage = 100, Points = 1 },
        new EczGrade { Grade = 2, Description = "Very Good", MinPercentage = 70, MaxPercentage = 74, Points = 2 },
        new EczGrade { Grade = 3, Description = "Good", MinPercentage = 65, MaxPercentage = 69, Points = 3 },
        new EczGrade { Grade = 4, Description = "Credit", MinPercentage = 60, MaxPercentage = 64, Points = 4 },
        new EczGrade { Grade = 5, Description = "Credit", MinPercentage = 55, MaxPercentage = 59, Points = 5 },
        new EczGrade { Grade = 6, Description = "Credit", MinPercentage = 50, MaxPercentage = 54, Points = 6 },
        new EczGrade { Grade = 7, Description = "Pass", MinPercentage = 45, MaxPercentage = 49, Points = 7 },
        new EczGrade { Grade = 8, Description = "Pass", MinPercentage = 40, MaxPercentage = 44, Points = 8 },
        new EczGrade { Grade = 9, Description = "Fail", MinPercentage = 0, MaxPercentage = 39, Points = 9 }
    };

    // Grade 12 Division System (based on best 7 subjects)
    public List<EczDivision> SchoolCertificateDivisions => new()
    {
        new EczDivision { Division = 1, Description = "First Division", MinPoints = 7, MaxPoints = 15 },
        new EczDivision { Division = 2, Description = "Second Division", MinPoints = 16, MaxPoints = 21 },
        new EczDivision { Division = 3, Description = "Third Division", MinPoints = 22, MaxPoints = 28 },
        new EczDivision { Division = 4, Description = "Fourth Division", MinPoints = 29, MaxPoints = 35 }
    };
}

public class EczProgram
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Grades { get; set; } = new();
    public string Qualification { get; set; } = string.Empty;
}

public class EczJuniorGrade
{
    public string Grade { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPercentage { get; set; }
    public int MaxPercentage { get; set; }
}

public class EczGrade
{
    public int Grade { get; set; }
    public string Description { get; set; } = string.Empty;
    public int MinPercentage { get; set; }
    public int MaxPercentage { get; set; }
    public int Points { get; set; }
}

public class EczDivision
{
    public int Division { get; set; }
    public string Description { get; set; } = string.Empty;
    public int MinPoints { get; set; }
    public int MaxPoints { get; set; }
}

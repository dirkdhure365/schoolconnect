namespace SchoolConnect.Institution.Domain.Entities;

public class InstituteSettings
{
    public string DefaultLanguage { get; set; } = "en";
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public string TimeFormat { get; set; } = "HH:mm";
    public string Currency { get; set; } = "USD";
    public GradingPolicy GradingPolicy { get; set; } = new();
    public AttendancePolicy AttendancePolicy { get; set; } = new();
    public List<string> EnabledFeatures { get; set; } = new();
}

public class GradingPolicy
{
    public string PassingGrade { get; set; } = "50";
    public string MaxGrade { get; set; } = "100";
    public List<GradeRange> GradeRanges { get; set; } = new();
}

public class GradeRange
{
    public string Symbol { get; set; } = string.Empty;
    public double MinPercentage { get; set; }
    public double MaxPercentage { get; set; }
}

public class AttendancePolicy
{
    public double MinimumAttendancePercentage { get; set; } = 75.0;
    public bool TrackLateArrivals { get; set; } = true;
    public int GracePeriodMinutes { get; set; } = 10;
}

namespace SchoolConnect.Enrolment.Application.ReadModels;

public class StudentEnrolmentSummaryReadModel
{
    public Guid Id { get; set; }
    
    public Guid StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string StudentFullName { get; set; } = string.Empty;
    public Guid StreamId { get; set; }
    public Guid CohortId { get; set; }
    public int CurrentGradeLevel { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public DateTime? WithdrawnAt { get; set; }
    public string? WithdrawalReason { get; set; }
    public DateTime LastUpdated { get; set; }
    public int Version { get; set; } // For optimistic concurrency
}

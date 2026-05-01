namespace SchoolConnect.Enrolment.Application.ReadModels;

public class StudentReadModel
{
    public Guid Id { get; set; }
    
    public Guid InstituteId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string? Nationality { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public DateTime? WithdrawnAt { get; set; }
    public string? WithdrawalReason { get; set; }
    public DateTime LastUpdated { get; set; }
    public int Version { get; set; } // For optimistic concurrency
}

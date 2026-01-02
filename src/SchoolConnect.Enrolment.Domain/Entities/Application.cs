using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;
using SchoolConnect.Enrolment.Domain.ValueObjects;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class Application : AggregateRoot
{
    public Guid AdmissionPeriodId { get; private set; }
    public string ApplicationNumber { get; private set; } = string.Empty;
    
    // Applicant Info
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public string? Nationality { get; private set; }
    public string? IdNumber { get; private set; }
    public PreviousSchoolInfo? PreviousSchool { get; private set; }
    
    // Guardian Info
    public GuardianInfo PrimaryGuardian { get; private set; } = null!;
    public GuardianInfo? SecondaryGuardian { get; private set; }
    
    // Application Details
    public Guid ProgramOfferingId { get; private set; }
    public Guid PreferredCentreId { get; private set; }
    public List<ApplicationDocument> Documents { get; private set; } = [];
    public Dictionary<string, string> AdditionalInfo { get; private set; } = new();
    
    // Status
    public ApplicationStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public Guid? ReviewedBy { get; private set; }
    public DateTime? ReviewedAt { get; private set; }
    public string? ReviewNotes { get; private set; }
    public string? RejectionReason { get; private set; }
    public int? WaitlistPosition { get; private set; }

    private Application() { }

    public static Application Create(
        Guid admissionPeriodId,
        string applicationNumber,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        Gender gender,
        GuardianInfo primaryGuardian,
        Guid programOfferingId,
        Guid preferredCentreId,
        string? nationality = null,
        string? idNumber = null,
        PreviousSchoolInfo? previousSchool = null,
        GuardianInfo? secondaryGuardian = null,
        Dictionary<string, string>? additionalInfo = null)
    {
        var application = new Application
        {
            AdmissionPeriodId = admissionPeriodId,
            ApplicationNumber = applicationNumber,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            Nationality = nationality,
            IdNumber = idNumber,
            PreviousSchool = previousSchool,
            PrimaryGuardian = primaryGuardian,
            SecondaryGuardian = secondaryGuardian,
            ProgramOfferingId = programOfferingId,
            PreferredCentreId = preferredCentreId,
            AdditionalInfo = additionalInfo ?? new(),
            Status = ApplicationStatus.Draft
        };

        return application;
    }

    public void Update(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        Gender gender,
        GuardianInfo primaryGuardian,
        string? nationality = null,
        string? idNumber = null,
        PreviousSchoolInfo? previousSchool = null,
        GuardianInfo? secondaryGuardian = null)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Nationality = nationality;
        IdNumber = idNumber;
        PreviousSchool = previousSchool;
        PrimaryGuardian = primaryGuardian;
        SecondaryGuardian = secondaryGuardian;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddDocument(ApplicationDocument document)
    {
        Documents.Add(document);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveDocument(Guid documentId)
    {
        Documents.RemoveAll(d => d.Id == documentId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Submit()
    {
        Status = ApplicationStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ApplicationSubmittedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Application),
            AdmissionPeriodId = AdmissionPeriodId,
            ApplicationNumber = ApplicationNumber,
            FirstName = FirstName,
            LastName = LastName,
            ProgramOfferingId = ProgramOfferingId,
            SubmittedAt = SubmittedAt
        });
    }

    public void Review(Guid reviewedBy, string? notes = null)
    {
        Status = ApplicationStatus.UnderReview;
        ReviewedBy = reviewedBy;
        ReviewedAt = DateTime.UtcNow;
        ReviewNotes = notes;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ApplicationReviewedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Application),
            ApplicationNumber = ApplicationNumber,
            ReviewedBy = reviewedBy,
            ReviewedAt = ReviewedAt.Value
        });
    }

    public void Approve(Guid approvedBy)
    {
        Status = ApplicationStatus.Approved;
        ReviewedBy = approvedBy;
        ReviewedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ApplicationApprovedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Application),
            ApplicationNumber = ApplicationNumber,
            ApprovedBy = approvedBy,
            ApprovedAt = ReviewedAt.Value
        });
    }

    public void Reject(Guid rejectedBy, string reason)
    {
        Status = ApplicationStatus.Rejected;
        ReviewedBy = rejectedBy;
        ReviewedAt = DateTime.UtcNow;
        RejectionReason = reason;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ApplicationRejectedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Application),
            ApplicationNumber = ApplicationNumber,
            RejectedBy = rejectedBy,
            Reason = reason,
            RejectedAt = ReviewedAt.Value
        });
    }

    public void Waitlist(int position)
    {
        Status = ApplicationStatus.Waitlisted;
        WaitlistPosition = position;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ApplicationWaitlistedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Application),
            ApplicationNumber = ApplicationNumber,
            WaitlistPosition = position,
            WaitlistedAt = DateTime.UtcNow
        });
    }

    public void MarkAsEnrolled()
    {
        Status = ApplicationStatus.Enrolled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw()
    {
        Status = ApplicationStatus.Withdrawn;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

public class ApplicationDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public bool IsVerified { get; set; }
}

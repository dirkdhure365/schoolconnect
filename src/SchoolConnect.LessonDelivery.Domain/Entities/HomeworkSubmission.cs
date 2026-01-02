using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class HomeworkSubmission : AggregateRoot
{
    public Guid HomeworkId { get; private set; }
    public Guid StudentId { get; private set; }
    public string StudentName { get; private set; } = string.Empty;
    public string StudentCode { get; private set; } = string.Empty;
    
    public string? Content { get; private set; }
    public List<string> AttachmentUrls { get; private set; } = [];
    public DateTime SubmittedAt { get; private set; }
    public bool IsLate { get; private set; }
    public int AttemptNumber { get; private set; }
    
    public SubmissionStatus Status { get; private set; }
    public decimal? MarksObtained { get; private set; }
    public decimal? Percentage { get; private set; }
    public string? Feedback { get; private set; }
    public string? AudioFeedbackUrl { get; private set; }
    public Guid? GradedBy { get; private set; }
    public DateTime? GradedAt { get; private set; }
    
    public bool ExtensionRequested { get; private set; }
    public DateTime? ExtensionRequestedDate { get; private set; }
    public string? ExtensionReason { get; private set; }
    public bool ExtensionGranted { get; private set; }
    public DateTime? ExtendedDueDate { get; private set; }

    private HomeworkSubmission() { }

    public static HomeworkSubmission Create(
        Guid homeworkId,
        Guid studentId,
        string studentName,
        string studentCode,
        DateTime dueDate,
        string? content = null,
        List<string>? attachmentUrls = null,
        int attemptNumber = 1)
    {
        var submission = new HomeworkSubmission
        {
            HomeworkId = homeworkId,
            StudentId = studentId,
            StudentName = studentName,
            StudentCode = studentCode,
            Content = content,
            AttachmentUrls = attachmentUrls ?? [],
            SubmittedAt = DateTime.UtcNow,
            IsLate = DateTime.UtcNow > dueDate,
            AttemptNumber = attemptNumber,
            Status = SubmissionStatus.Submitted
        };

        submission.AddDomainEvent(new HomeworkSubmittedEvent
        {
            AggregateId = submission.Id,
            AggregateType = nameof(HomeworkSubmission),
            Version = submission.Version,
            HomeworkId = homeworkId,
            StudentId = studentId,
            SubmittedAt = submission.SubmittedAt
        });

        return submission;
    }

    public void Grade(decimal marksObtained, decimal maxMarks, Guid gradedBy, string? feedback = null, string? audioFeedbackUrl = null)
    {
        MarksObtained = marksObtained;
        Percentage = maxMarks > 0 ? (marksObtained / maxMarks) * 100 : 0;
        Feedback = feedback;
        AudioFeedbackUrl = audioFeedbackUrl;
        GradedBy = gradedBy;
        GradedAt = DateTime.UtcNow;
        Status = SubmissionStatus.Graded;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new HomeworkGradedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(HomeworkSubmission),
            Version = Version,
            HomeworkId = HomeworkId,
            StudentId = StudentId,
            MarksObtained = marksObtained,
            GradedBy = gradedBy
        });
    }

    public void RequestExtension(string reason)
    {
        ExtensionRequested = true;
        ExtensionRequestedDate = DateTime.UtcNow;
        ExtensionReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void GrantExtension(DateTime newDueDate)
    {
        ExtensionGranted = true;
        ExtendedDueDate = newDueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

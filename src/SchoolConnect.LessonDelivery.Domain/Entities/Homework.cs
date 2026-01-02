using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class Homework : AggregateRoot
{
    public Guid? LessonSessionId { get; private set; }
    public Guid ClassId { get; private set; }
    public string ClassName { get; private set; } = string.Empty;
    public Guid SubjectId { get; private set; }
    public string SubjectName { get; private set; } = string.Empty;
    public Guid TeacherId { get; private set; }
    public string TeacherName { get; private set; } = string.Empty;
    
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Instructions { get; private set; }
    public List<Guid> CurriculumTopicIds { get; private set; } = [];
    
    public DateTime AssignedDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public int? MaxMarks { get; private set; }
    public decimal? Weighting { get; private set; }
    
    public List<string> AttachmentUrls { get; private set; } = [];
    public bool AllowLateSubmission { get; private set; }
    public int? LatePenaltyPercent { get; private set; }
    public int? MaxAttempts { get; private set; }
    
    public HomeworkStatus Status { get; private set; }
    public int SubmissionCount { get; private set; }
    public int GradedCount { get; private set; }

    private Homework() { }

    public static Homework Create(
        Guid classId,
        string className,
        Guid subjectId,
        string subjectName,
        Guid teacherId,
        string teacherName,
        string title,
        DateTime dueDate,
        Guid? lessonSessionId = null,
        string? description = null,
        string? instructions = null,
        int? maxMarks = null,
        bool allowLateSubmission = false)
    {
        var homework = new Homework
        {
            LessonSessionId = lessonSessionId,
            ClassId = classId,
            ClassName = className,
            SubjectId = subjectId,
            SubjectName = subjectName,
            TeacherId = teacherId,
            TeacherName = teacherName,
            Title = title,
            Description = description,
            Instructions = instructions,
            AssignedDate = DateTime.UtcNow,
            DueDate = dueDate,
            MaxMarks = maxMarks,
            AllowLateSubmission = allowLateSubmission,
            Status = HomeworkStatus.Draft
        };

        homework.AddDomainEvent(new HomeworkAssignedEvent
        {
            AggregateId = homework.Id,
            AggregateType = nameof(Homework),
            Version = homework.Version,
            ClassId = classId,
            Title = title,
            DueDate = dueDate
        });

        return homework;
    }

    public void Update(string title, DateTime dueDate, string? description = null, string? instructions = null, int? maxMarks = null)
    {
        Title = title;
        DueDate = dueDate;
        Description = description;
        Instructions = instructions;
        MaxMarks = maxMarks;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new HomeworkUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Homework),
            Version = Version,
            Title = title
        });
    }

    public void Publish()
    {
        Status = HomeworkStatus.Published;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementSubmissionCount()
    {
        SubmissionCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementGradedCount()
    {
        GradedCount++;
        UpdatedAt = DateTime.UtcNow;
        
        if (GradedCount >= SubmissionCount && SubmissionCount > 0)
        {
            Status = HomeworkStatus.Graded;
        }
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

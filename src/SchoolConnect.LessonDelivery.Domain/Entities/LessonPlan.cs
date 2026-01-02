using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;
using SchoolConnect.LessonDelivery.Domain.ValueObjects;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class LessonPlan : AggregateRoot
{
    public Guid ClassId { get; private set; }
    public string ClassName { get; private set; } = string.Empty;
    public Guid SubjectId { get; private set; }
    public string SubjectName { get; private set; } = string.Empty;
    public Guid TeacherId { get; private set; }
    public string TeacherName { get; private set; } = string.Empty;
    
    // Content
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public List<Guid> CurriculumTopicIds { get; private set; } = [];
    public List<LearningObjective> LearningObjectives { get; private set; } = [];
    public List<LessonPlanActivity> Activities { get; private set; } = [];
    public List<LessonPlanResource> Resources { get; private set; } = [];
    public string? Prerequisites { get; private set; }
    public string? TeacherNotes { get; private set; }
    
    // Timing
    public int DurationMinutes { get; private set; }
    public int? GradeLevel { get; private set; }
    public int? TermNumber { get; private set; }
    public int? WeekNumber { get; private set; }
    
    // Status & Approval
    public LessonPlanStatus Status { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string? ApprovalNotes { get; private set; }
    
    // Sharing
    public bool IsShared { get; private set; }
    public List<Guid> SharedWithTeacherIds { get; private set; } = [];
    public Guid? ClonedFromId { get; private set; }

    private LessonPlan() { }

    public static LessonPlan Create(
        Guid classId,
        string className,
        Guid subjectId,
        string subjectName,
        Guid teacherId,
        string teacherName,
        string title,
        int durationMinutes,
        string? description = null)
    {
        var lessonPlan = new LessonPlan
        {
            ClassId = classId,
            ClassName = className,
            SubjectId = subjectId,
            SubjectName = subjectName,
            TeacherId = teacherId,
            TeacherName = teacherName,
            Title = title,
            Description = description,
            DurationMinutes = durationMinutes,
            Status = LessonPlanStatus.Draft
        };

        lessonPlan.AddDomainEvent(new LessonPlanCreatedEvent
        {
            AggregateId = lessonPlan.Id,
            AggregateType = nameof(LessonPlan),
            Version = lessonPlan.Version,
            ClassId = classId,
            TeacherId = teacherId,
            Title = title
        });

        return lessonPlan;
    }

    public void Update(string title, string? description, int durationMinutes)
    {
        Title = title;
        Description = description;
        DurationMinutes = durationMinutes;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonPlanUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonPlan),
            Version = Version,
            Title = title
        });
    }

    public void AddActivity(LessonPlanActivity activity)
    {
        Activities.Add(activity);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveActivity(Guid activityId)
    {
        Activities.RemoveAll(a => a.Id == activityId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddResource(LessonPlanResource resource)
    {
        Resources.Add(resource);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveResource(Guid resourceId)
    {
        Resources.RemoveAll(r => r.Id == resourceId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void SubmitForApproval(Guid submittedBy)
    {
        Status = LessonPlanStatus.PendingApproval;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonPlanSubmittedForApprovalEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonPlan),
            Version = Version,
            SubmittedBy = submittedBy
        });
    }

    public void Approve(Guid approvedBy, string? notes = null)
    {
        Status = LessonPlanStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        ApprovalNotes = notes;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonPlanApprovedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonPlan),
            Version = Version,
            ApprovedBy = approvedBy,
            Notes = notes
        });
    }

    public void Reject(Guid rejectedBy, string? reason = null)
    {
        Status = LessonPlanStatus.Rejected;
        ApprovalNotes = reason;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonPlanRejectedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonPlan),
            Version = Version,
            RejectedBy = rejectedBy,
            Reason = reason
        });
    }

    public void Share(List<Guid> teacherIds)
    {
        IsShared = true;
        SharedWithTeacherIds = teacherIds;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonPlanSharedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonPlan),
            Version = Version,
            SharedWithTeacherIds = teacherIds
        });
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

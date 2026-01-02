using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;
using SchoolConnect.LessonDelivery.Domain.ValueObjects;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class ScheduledLesson : AggregateRoot
{
    public Guid? LessonPlanId { get; private set; }
    public Guid ClassId { get; private set; }
    public string ClassName { get; private set; } = string.Empty;
    public Guid SubjectId { get; private set; }
    public string SubjectName { get; private set; } = string.Empty;
    public Guid TeacherId { get; private set; }
    public string TeacherName { get; private set; } = string.Empty;
    public Guid? FacilityId { get; private set; }
    public string? FacilityName { get; private set; }
    
    public string? Title { get; private set; }
    public DateTime ScheduledStart { get; private set; }
    public DateTime ScheduledEnd { get; private set; }
    public RecurrenceRule? Recurrence { get; private set; }
    public Guid? RecurrenceGroupId { get; private set; }
    
    public LessonStatus Status { get; private set; }
    public string? CancellationReason { get; private set; }
    public Guid? SubstituteTeacherId { get; private set; }

    private ScheduledLesson() { }

    public static ScheduledLesson Create(
        Guid classId,
        string className,
        Guid subjectId,
        string subjectName,
        Guid teacherId,
        string teacherName,
        DateTime scheduledStart,
        DateTime scheduledEnd,
        Guid? lessonPlanId = null,
        Guid? facilityId = null,
        string? facilityName = null,
        string? title = null)
    {
        var lesson = new ScheduledLesson
        {
            LessonPlanId = lessonPlanId,
            ClassId = classId,
            ClassName = className,
            SubjectId = subjectId,
            SubjectName = subjectName,
            TeacherId = teacherId,
            TeacherName = teacherName,
            FacilityId = facilityId,
            FacilityName = facilityName,
            Title = title,
            ScheduledStart = scheduledStart,
            ScheduledEnd = scheduledEnd,
            Status = LessonStatus.Scheduled
        };

        lesson.AddDomainEvent(new LessonScheduledEvent
        {
            AggregateId = lesson.Id,
            AggregateType = nameof(ScheduledLesson),
            Version = lesson.Version,
            ClassId = classId,
            ScheduledStart = scheduledStart,
            ScheduledEnd = scheduledEnd
        });

        return lesson;
    }

    public void Reschedule(DateTime newStart, DateTime newEnd)
    {
        var oldStart = ScheduledStart;
        
        ScheduledStart = newStart;
        ScheduledEnd = newEnd;
        Status = LessonStatus.Rescheduled;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonRescheduledEvent
        {
            AggregateId = Id,
            AggregateType = nameof(ScheduledLesson),
            Version = Version,
            OldStart = oldStart,
            NewStart = newStart,
            NewEnd = newEnd
        });
    }

    public void Cancel(string? reason = null)
    {
        Status = LessonStatus.Cancelled;
        CancellationReason = reason;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonCancelledEvent
        {
            AggregateId = Id,
            AggregateType = nameof(ScheduledLesson),
            Version = Version,
            Reason = reason
        });
    }

    public void MarkAsInProgress()
    {
        Status = LessonStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = LessonStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

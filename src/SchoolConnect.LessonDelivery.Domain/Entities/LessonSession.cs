using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;
using SchoolConnect.LessonDelivery.Domain.ValueObjects;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class LessonSession : AggregateRoot
{
    public Guid ScheduledLessonId { get; private set; }
    public Guid? LessonPlanId { get; private set; }
    public Guid ClassId { get; private set; }
    public Guid TeacherId { get; private set; }
    
    public DateTime ActualStart { get; private set; }
    public DateTime? ActualEnd { get; private set; }
    public int? ActualDurationMinutes { get; private set; }
    
    public List<TopicCoverage> TopicsCovered { get; private set; } = [];
    public string? Notes { get; private set; }
    public string? Reflection { get; private set; }
    public string? AudioRecordingUrl { get; private set; }
    public int? EffectivenessRating { get; private set; }
    public string? FollowUpActions { get; private set; }
    
    public SessionStatus Status { get; private set; }
    public int AttendeeCount { get; private set; }
    public int AbsentCount { get; private set; }

    private LessonSession() { }

    public static LessonSession Create(
        Guid scheduledLessonId,
        Guid classId,
        Guid teacherId,
        Guid? lessonPlanId = null)
    {
        var session = new LessonSession
        {
            ScheduledLessonId = scheduledLessonId,
            LessonPlanId = lessonPlanId,
            ClassId = classId,
            TeacherId = teacherId,
            ActualStart = DateTime.UtcNow,
            Status = SessionStatus.InProgress
        };

        session.AddDomainEvent(new LessonStartedEvent
        {
            AggregateId = session.Id,
            AggregateType = nameof(LessonSession),
            Version = session.Version,
            ScheduledLessonId = scheduledLessonId,
            ActualStart = session.ActualStart
        });

        return session;
    }

    public void End(string? notes = null, string? reflection = null)
    {
        ActualEnd = DateTime.UtcNow;
        ActualDurationMinutes = (int)(ActualEnd.Value - ActualStart).TotalMinutes;
        Status = SessionStatus.Completed;
        Notes = notes;
        Reflection = reflection;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LessonCompletedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonSession),
            Version = Version,
            ScheduledLessonId = ScheduledLessonId,
            ActualEnd = ActualEnd.Value,
            DurationMinutes = ActualDurationMinutes.Value
        });
    }

    public void AddAudioRecording(string url)
    {
        AudioRecordingUrl = url;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new AudioRecordingAddedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(LessonSession),
            Version = Version,
            SessionId = Id,
            Url = url
        });
    }

    public void UpdateAttendanceCounts(int attendeeCount, int absentCount)
    {
        AttendeeCount = attendeeCount;
        AbsentCount = absentCount;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

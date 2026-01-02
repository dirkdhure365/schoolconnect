using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class CurriculumCoverage : AggregateRoot
{
    public Guid ClassId { get; private set; }
    public Guid SubjectId { get; private set; }
    public Guid CurriculumTopicId { get; private set; }
    public string TopicName { get; private set; } = string.Empty;
    public string? TopicCode { get; private set; }
    public int? TermNumber { get; private set; }
    
    public decimal PlannedHours { get; private set; }
    public decimal ActualHours { get; private set; }
    public decimal RemainingHours => Math.Max(0, PlannedHours - ActualHours);
    public decimal ProgressPercentage => PlannedHours > 0 ? (ActualHours / PlannedHours) * 100 : 0;
    
    public CoverageStatus Status { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public List<Guid> LessonSessionIds { get; private set; } = [];
    
    public string? Notes { get; private set; }

    private CurriculumCoverage() { }

    public static CurriculumCoverage Create(
        Guid classId,
        Guid subjectId,
        Guid curriculumTopicId,
        string topicName,
        decimal plannedHours,
        string? topicCode = null,
        int? termNumber = null)
    {
        return new CurriculumCoverage
        {
            ClassId = classId,
            SubjectId = subjectId,
            CurriculumTopicId = curriculumTopicId,
            TopicName = topicName,
            TopicCode = topicCode,
            TermNumber = termNumber,
            PlannedHours = plannedHours,
            ActualHours = 0,
            Status = CoverageStatus.NotStarted
        };
    }

    public void RecordTime(decimal hours, Guid lessonSessionId)
    {
        if (Status == CoverageStatus.NotStarted)
        {
            Status = CoverageStatus.InProgress;
            StartedAt = DateTime.UtcNow;
        }

        ActualHours += hours;
        
        if (!LessonSessionIds.Contains(lessonSessionId))
        {
            LessonSessionIds.Add(lessonSessionId);
        }

        if (ActualHours >= PlannedHours && Status != CoverageStatus.Completed)
        {
            Status = CoverageStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CurriculumTopicCoveredEvent
        {
            AggregateId = Id,
            AggregateType = nameof(CurriculumCoverage),
            Version = Version,
            TopicId = CurriculumTopicId,
            ClassId = ClassId,
            HoursCovered = hours
        });

        AddDomainEvent(new CurriculumCoverageUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(CurriculumCoverage),
            Version = Version,
            TopicId = CurriculumTopicId,
            ActualHours = ActualHours
        });
    }

    public void UpdatePlannedHours(decimal plannedHours)
    {
        PlannedHours = plannedHours;
        
        // Re-evaluate status
        if (ActualHours >= PlannedHours && Status == CoverageStatus.InProgress)
        {
            Status = CoverageStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }
        else if (ActualHours < PlannedHours && Status == CoverageStatus.Completed)
        {
            Status = CoverageStatus.InProgress;
            CompletedAt = null;
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsSkipped(string? reason = null)
    {
        Status = CoverageStatus.Skipped;
        Notes = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;
using SchoolConnect.LessonDelivery.Domain.Events;

namespace SchoolConnect.LessonDelivery.Domain.Entities;

public class Attendance : AggregateRoot
{
    public Guid LessonSessionId { get; private set; }
    public Guid ClassId { get; private set; }
    public Guid StudentId { get; private set; }
    public string StudentName { get; private set; } = string.Empty;
    public string StudentCode { get; private set; } = string.Empty;
    
    public AttendanceStatus Status { get; private set; }
    public DateTime? ArrivalTime { get; private set; }
    public int? LateByMinutes { get; private set; }
    public string? AbsenceReason { get; private set; }
    public bool IsExcused { get; private set; }
    public string? Notes { get; private set; }
    
    public Guid MarkedBy { get; private set; }
    public DateTime MarkedAt { get; private set; }

    private Attendance() { }

    public static Attendance Create(
        Guid lessonSessionId,
        Guid classId,
        Guid studentId,
        string studentName,
        string studentCode,
        AttendanceStatus status,
        Guid markedBy,
        DateTime? arrivalTime = null,
        int? lateByMinutes = null,
        string? absenceReason = null,
        bool isExcused = false,
        string? notes = null)
    {
        var attendance = new Attendance
        {
            LessonSessionId = lessonSessionId,
            ClassId = classId,
            StudentId = studentId,
            StudentName = studentName,
            StudentCode = studentCode,
            Status = status,
            ArrivalTime = arrivalTime,
            LateByMinutes = lateByMinutes,
            AbsenceReason = absenceReason,
            IsExcused = isExcused,
            Notes = notes,
            MarkedBy = markedBy,
            MarkedAt = DateTime.UtcNow
        };

        attendance.AddDomainEvent(new AttendanceRecordedEvent
        {
            AggregateId = attendance.Id,
            AggregateType = nameof(Attendance),
            Version = attendance.Version,
            StudentId = studentId,
            SessionId = lessonSessionId,
            Status = status
        });

        if (status == AttendanceStatus.Absent)
        {
            attendance.AddDomainEvent(new StudentMarkedAbsentEvent
            {
                AggregateId = attendance.Id,
                AggregateType = nameof(Attendance),
                Version = attendance.Version,
                StudentId = studentId,
                SessionId = lessonSessionId,
                Reason = absenceReason
            });
        }
        else if (status == AttendanceStatus.Late)
        {
            attendance.AddDomainEvent(new StudentMarkedLateEvent
            {
                AggregateId = attendance.Id,
                AggregateType = nameof(Attendance),
                Version = attendance.Version,
                StudentId = studentId,
                SessionId = lessonSessionId,
                LateByMinutes = lateByMinutes ?? 0
            });
        }

        return attendance;
    }

    public void Update(
        AttendanceStatus status,
        DateTime? arrivalTime = null,
        int? lateByMinutes = null,
        string? absenceReason = null,
        bool? isExcused = null,
        string? notes = null)
    {
        Status = status;
        if (arrivalTime.HasValue) ArrivalTime = arrivalTime;
        if (lateByMinutes.HasValue) LateByMinutes = lateByMinutes;
        if (absenceReason != null) AbsenceReason = absenceReason;
        if (isExcused.HasValue) IsExcused = isExcused.Value;
        if (notes != null) Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

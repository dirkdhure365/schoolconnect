using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record AttendanceRecordedEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid SessionId { get; init; }
    public AttendanceStatus Status { get; init; }
}

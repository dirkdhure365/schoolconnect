using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record AudioRecordingAddedEvent : DomainEvent
{
    public Guid SessionId { get; init; }
    public string Url { get; init; } = string.Empty;
}

using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.LessonDelivery.Domain.Enums;

namespace SchoolConnect.LessonDelivery.Domain.Events;

public record LessonArtifactAddedEvent : DomainEvent
{
    public Guid SessionId { get; init; }
    public ArtifactType Type { get; init; }
    public string Name { get; init; } = string.Empty;
}

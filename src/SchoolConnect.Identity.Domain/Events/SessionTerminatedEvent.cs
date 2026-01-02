using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record SessionTerminatedEvent : DomainEvent
{
    public Guid SessionId { get; init; }

    public SessionTerminatedEvent(Guid aggregateId, Guid sessionId, int version)
    {
        AggregateId = aggregateId;
        SessionId = sessionId;
        AggregateType = "Session";
        Version = version;
    }
}

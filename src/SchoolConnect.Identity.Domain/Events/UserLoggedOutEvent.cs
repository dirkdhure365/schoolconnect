using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record UserLoggedOutEvent : DomainEvent
{
    public Guid SessionId { get; init; }

    public UserLoggedOutEvent(Guid aggregateId, Guid sessionId, int version)
    {
        AggregateId = aggregateId;
        SessionId = sessionId;
        AggregateType = "User";
        Version = version;
    }
}

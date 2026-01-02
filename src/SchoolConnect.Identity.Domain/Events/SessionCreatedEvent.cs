using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record SessionCreatedEvent : DomainEvent
{
    public Guid UserId { get; init; }
    public string IpAddress { get; init; }

    public SessionCreatedEvent(Guid aggregateId, Guid userId, string ipAddress, int version)
    {
        AggregateId = aggregateId;
        UserId = userId;
        IpAddress = ipAddress;
        AggregateType = "Session";
        Version = version;
    }
}

using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record RoleDeletedEvent : DomainEvent
{
    public RoleDeletedEvent(Guid aggregateId, int version)
    {
        AggregateId = aggregateId;
        AggregateType = "Role";
        Version = version;
    }
}

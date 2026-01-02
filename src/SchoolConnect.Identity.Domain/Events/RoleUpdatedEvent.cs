using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record RoleUpdatedEvent : DomainEvent
{
    public string Name { get; init; }

    public RoleUpdatedEvent(Guid aggregateId, string name, int version)
    {
        AggregateId = aggregateId;
        Name = name;
        AggregateType = "Role";
        Version = version;
    }
}

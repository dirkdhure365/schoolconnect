using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record RoleCreatedEvent : DomainEvent
{
    public string Name { get; init; }
    public bool IsSystemRole { get; init; }

    public RoleCreatedEvent(Guid aggregateId, string name, bool isSystemRole, int version)
    {
        AggregateId = aggregateId;
        Name = name;
        IsSystemRole = isSystemRole;
        AggregateType = "Role";
        Version = version;
    }
}

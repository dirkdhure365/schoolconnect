using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record RoleAssignedEvent : DomainEvent
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }

    public RoleAssignedEvent(Guid aggregateId, Guid userId, Guid roleId, int version)
    {
        AggregateId = aggregateId;
        UserId = userId;
        RoleId = roleId;
        AggregateType = "UserRole";
        Version = version;
    }
}

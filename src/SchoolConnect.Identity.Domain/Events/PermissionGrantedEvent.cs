using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record PermissionGrantedEvent : DomainEvent
{
    public Guid RoleId { get; init; }
    public Guid PermissionId { get; init; }

    public PermissionGrantedEvent(Guid aggregateId, Guid roleId, Guid permissionId, int version)
    {
        AggregateId = aggregateId;
        RoleId = roleId;
        PermissionId = permissionId;
        AggregateType = "Role";
        Version = version;
    }
}

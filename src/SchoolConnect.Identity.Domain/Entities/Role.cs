using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Identity.Domain.Events;

namespace SchoolConnect.Identity.Domain.Entities;

public class Role : AggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsSystemRole { get; private set; }
    public List<Guid> PermissionIds { get; private set; } = [];

    private Role() { }

    public Role(string name, string? description, bool isSystemRole = false)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        IsSystemRole = isSystemRole;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Apply(new RoleCreatedEvent(Id, Name, IsSystemRole, Version));
    }

    public void Update(string name, string? description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        UpdatedAt = DateTime.UtcNow;

        Apply(new RoleUpdatedEvent(Id, Name, Version));
    }

    public void AddPermission(Guid permissionId)
    {
        if (!PermissionIds.Contains(permissionId))
        {
            PermissionIds.Add(permissionId);
            UpdatedAt = DateTime.UtcNow;
            Apply(new PermissionGrantedEvent(Id, Id, permissionId, Version));
        }
    }

    public void RemovePermission(Guid permissionId)
    {
        if (PermissionIds.Remove(permissionId))
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Delete()
    {
        if (IsSystemRole)
            throw new InvalidOperationException("Cannot delete system roles");

        Apply(new RoleDeletedEvent(Id, Version));
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - apply events to rebuild state
    }
}

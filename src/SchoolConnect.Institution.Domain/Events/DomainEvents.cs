using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Events;

// Institute Events
public record InstituteCreatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public InstituteType Type { get; init; }
    public string Country { get; init; } = string.Empty;
}

public record InstituteUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}

public record InstituteDeactivatedEvent : DomainEvent { }

// Centre Events
public record CentreCreatedEvent : DomainEvent
{
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
}

public record CentreUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
}

public record CentreDeactivatedEvent : DomainEvent { }

// Facility Events
public record FacilityCreatedEvent : DomainEvent
{
    public Guid CentreId { get; init; }
    public string Name { get; init; } = string.Empty;
    public FacilityType Type { get; init; }
}

public record FacilityUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
}

public record FacilityBookedEvent : DomainEvent
{
    public Guid FacilityId { get; init; }
    public Guid BookedBy { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}

public record FacilityBookingCancelledEvent : DomainEvent
{
    public Guid FacilityId { get; init; }
    public string? Reason { get; init; }
}

// Resource Events
public record ResourceCreatedEvent : DomainEvent
{
    public Guid CentreId { get; init; }
    public string Name { get; init; } = string.Empty;
    public ResourceType Type { get; init; }
}

public record ResourceUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
}

public record ResourceAllocatedEvent : DomainEvent
{
    public Guid ResourceId { get; init; }
    public Guid AllocatedToId { get; init; }
    public string AllocatedToType { get; init; } = string.Empty;
}

public record ResourceReturnedEvent : DomainEvent
{
    public Guid ResourceId { get; init; }
    public ResourceCondition ConditionOnReturn { get; init; }
}

public record ResourceDamagedEvent : DomainEvent
{
    public Guid ResourceId { get; init; }
    public ResourceCondition Condition { get; init; }
    public string Notes { get; init; } = string.Empty;
}

// Staff Events
public record StaffOnboardedEvent : DomainEvent
{
    public Guid InstituteId { get; init; }
    public string EmployeeCode { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}

public record StaffUpdatedEvent : DomainEvent
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}

public record StaffOffboardedEvent : DomainEvent
{
    public DateTime TerminationDate { get; init; }
}

public record StaffAssignedToCentreEvent : DomainEvent
{
    public Guid StaffMemberId { get; init; }
    public Guid CentreId { get; init; }
    public bool IsPrimary { get; init; }
}

public record StaffRemovedFromCentreEvent : DomainEvent
{
    public Guid StaffMemberId { get; init; }
    public Guid CentreId { get; init; }
}

// Team Events
public record TeamCreatedEvent : DomainEvent
{
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public TeamType Type { get; init; }
}

public record TeamUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
}

public record TeamDeletedEvent : DomainEvent { }

public record TeamMemberAddedEvent : DomainEvent
{
    public Guid TeamId { get; init; }
    public Guid StaffMemberId { get; init; }
}

public record TeamMemberRemovedEvent : DomainEvent
{
    public Guid TeamId { get; init; }
    public Guid StaffMemberId { get; init; }
}

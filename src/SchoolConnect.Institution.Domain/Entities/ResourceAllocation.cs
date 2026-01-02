using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;

namespace SchoolConnect.Institution.Domain.Entities;

public class ResourceAllocation : AggregateRoot
{
    public Guid ResourceId { get; private set; }
    public Guid AllocatedToId { get; private set; }
    public string AllocatedToType { get; private set; } = string.Empty; // "Staff" or "Student"
    public string AllocatedToName { get; private set; } = string.Empty;
    public Guid AllocatedBy { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public DateTime? ReturnedDate { get; private set; }
    public ResourceCondition? ConditionOnReturn { get; private set; }
    public string? Notes { get; private set; }
    public AllocationStatus Status { get; private set; }

    private ResourceAllocation() { }

    public static ResourceAllocation Create(
        Guid resourceId,
        Guid allocatedToId,
        string allocatedToType,
        string allocatedToName,
        Guid allocatedBy,
        DateTime startDate,
        DateTime? endDate = null,
        string? notes = null)
    {
        var allocation = new ResourceAllocation
        {
            ResourceId = resourceId,
            AllocatedToId = allocatedToId,
            AllocatedToType = allocatedToType,
            AllocatedToName = allocatedToName,
            AllocatedBy = allocatedBy,
            StartDate = startDate,
            EndDate = endDate,
            Notes = notes,
            Status = AllocationStatus.Active
        };

        allocation.AddDomainEvent(new ResourceAllocatedEvent
        {
            AggregateId = allocation.Id,
            AggregateType = nameof(ResourceAllocation),
            ResourceId = resourceId,
            AllocatedToId = allocatedToId,
            AllocatedToType = allocatedToType
        });

        return allocation;
    }

    public void Return(ResourceCondition conditionOnReturn, string? notes = null)
    {
        Status = AllocationStatus.Returned;
        ReturnedDate = DateTime.UtcNow;
        ConditionOnReturn = conditionOnReturn;
        if (notes != null) Notes = notes;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ResourceReturnedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(ResourceAllocation),
            ResourceId = ResourceId,
            ConditionOnReturn = conditionOnReturn
        });
    }

    public void MarkAsOverdue()
    {
        Status = AllocationStatus.Overdue;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsLost()
    {
        Status = AllocationStatus.Lost;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

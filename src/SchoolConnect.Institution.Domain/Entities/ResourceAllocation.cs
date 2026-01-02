using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

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
            EventType = nameof(ResourceAllocatedEvent),
            ResourceId = resourceId,
            AllocatedToId = allocatedToId,
            AllocatedToType = allocatedToType
        });
        
        return allocation;
    }
    
    public void Return(ResourceCondition conditionOnReturn, string? notes = null)
    {
        ReturnedDate = DateTime.UtcNow;
        ConditionOnReturn = conditionOnReturn;
        Status = AllocationStatus.Returned;
        if (notes != null) Notes = notes;
        MarkAsUpdated();
        
        AddDomainEvent(new ResourceReturnedEvent
        {
            AggregateId = Id,
            EventType = nameof(ResourceReturnedEvent),
            ResourceId = ResourceId,
            ConditionOnReturn = conditionOnReturn
        });
    }
    
    public void MarkAsOverdue()
    {
        Status = AllocationStatus.Overdue;
        MarkAsUpdated();
    }
    
    public void MarkAsLost()
    {
        Status = AllocationStatus.Lost;
        MarkAsUpdated();
    }
    
    public void ReportDamage(ResourceCondition condition, string notes)
    {
        ConditionOnReturn = condition;
        Notes = notes;
        MarkAsUpdated();
        
        AddDomainEvent(new ResourceDamagedEvent
        {
            AggregateId = Id,
            EventType = nameof(ResourceDamagedEvent),
            ResourceId = ResourceId,
            Condition = condition,
            Notes = notes
        });
    }
}

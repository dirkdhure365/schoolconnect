using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Entities;

public class Resource : AggregateRoot
{
    public Guid CentreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ResourceType Type { get; private set; }
    public string? SerialNumber { get; private set; }
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public ResourceCondition Condition { get; private set; }
    public ResourceStatus Status { get; private set; }
    public DateTime? AcquisitionDate { get; private set; }
    public decimal? Value { get; private set; }
    public string? Currency { get; private set; }
    public string? Location { get; private set; }
    public Dictionary<string, string> Attributes { get; private set; } = new();
    
    private Resource() { }
    
    public static Resource Create(
        Guid centreId,
        string name,
        ResourceType type,
        string? serialNumber = null,
        string? description = null,
        DateTime? acquisitionDate = null,
        decimal? value = null,
        string? currency = null,
        string? location = null)
    {
        var resource = new Resource
        {
            CentreId = centreId,
            Name = name,
            Type = type,
            SerialNumber = serialNumber,
            Description = description,
            AcquisitionDate = acquisitionDate,
            Value = value,
            Currency = currency,
            Location = location,
            Condition = ResourceCondition.New,
            Status = ResourceStatus.Available
        };
        
        resource.AddDomainEvent(new ResourceCreatedEvent
        {
            AggregateId = resource.Id,
            EventType = nameof(ResourceCreatedEvent),
            CentreId = centreId,
            Name = name,
            Type = type
        });
        
        return resource;
    }
    
    public void Update(
        string name,
        string? description = null,
        decimal? value = null,
        string? location = null)
    {
        Name = name;
        Description = description;
        Value = value;
        Location = location;
        MarkAsUpdated();
        
        AddDomainEvent(new ResourceUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(ResourceUpdatedEvent),
            Name = name
        });
    }
    
    public void UpdateCondition(ResourceCondition condition)
    {
        Condition = condition;
        MarkAsUpdated();
    }
    
    public void SetAvailable()
    {
        Status = ResourceStatus.Available;
        MarkAsUpdated();
    }
    
    public void SetAllocated()
    {
        Status = ResourceStatus.Allocated;
        MarkAsUpdated();
    }
    
    public void SetUnderRepair()
    {
        Status = ResourceStatus.UnderRepair;
        MarkAsUpdated();
    }
    
    public void MarkAsLost()
    {
        Status = ResourceStatus.Lost;
        MarkAsUpdated();
    }
    
    public void Retire()
    {
        Status = ResourceStatus.Retired;
        Condition = ResourceCondition.Retired;
        MarkAsUpdated();
    }
}

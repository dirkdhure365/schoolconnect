using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;

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
        ResourceCondition condition,
        string? serialNumber = null,
        string? description = null,
        string? imageUrl = null,
        DateTime? acquisitionDate = null,
        decimal? value = null,
        string? currency = null,
        string? location = null,
        Dictionary<string, string>? attributes = null)
    {
        var resource = new Resource
        {
            CentreId = centreId,
            Name = name,
            Type = type,
            SerialNumber = serialNumber,
            Description = description,
            ImageUrl = imageUrl,
            Condition = condition,
            Status = ResourceStatus.Available,
            AcquisitionDate = acquisitionDate,
            Value = value,
            Currency = currency,
            Location = location,
            Attributes = attributes ?? new()
        };

        resource.AddDomainEvent(new ResourceCreatedEvent
        {
            AggregateId = resource.Id,
            AggregateType = nameof(Resource),
            CentreId = centreId,
            Name = name,
            Type = type
        });

        return resource;
    }

    public void Update(
        string name,
        string? serialNumber = null,
        string? description = null,
        string? imageUrl = null,
        decimal? value = null,
        string? currency = null,
        string? location = null,
        Dictionary<string, string>? attributes = null)
    {
        Name = name;
        SerialNumber = serialNumber;
        Description = description;
        ImageUrl = imageUrl;
        Value = value;
        Currency = currency;
        Location = location;
        if (attributes != null) Attributes = attributes;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ResourceUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Resource),
            Name = name
        });
    }

    public void UpdateStatus(ResourceStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCondition(ResourceCondition condition)
    {
        Condition = condition;
        UpdatedAt = DateTime.UtcNow;

        if (condition == ResourceCondition.Damaged)
        {
            AddDomainEvent(new ResourceDamagedEvent
            {
                AggregateId = Id,
                AggregateType = nameof(Resource),
                Condition = condition
            });
        }
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

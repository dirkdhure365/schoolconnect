using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Entities;

public class Facility : AggregateRoot
{
    public Guid CentreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Code { get; private set; }
    public FacilityType Type { get; private set; }
    public int Capacity { get; private set; }
    public string? Description { get; private set; }
    public List<string> Amenities { get; private set; } = new();
    public string? ImageUrl { get; private set; }
    public FacilityStatus Status { get; private set; }
    public bool IsBookable { get; private set; }
    public BookingRules? BookingRules { get; private set; }
    
    private Facility() { }
    
    public static Facility Create(
        Guid centreId,
        string name,
        FacilityType type,
        int capacity,
        bool isBookable,
        string? code = null,
        string? description = null,
        List<string>? amenities = null,
        BookingRules? bookingRules = null)
    {
        var facility = new Facility
        {
            CentreId = centreId,
            Name = name,
            Code = code,
            Type = type,
            Capacity = capacity,
            Description = description,
            Amenities = amenities ?? new List<string>(),
            IsBookable = isBookable,
            BookingRules = bookingRules,
            Status = FacilityStatus.Available
        };
        
        facility.AddDomainEvent(new FacilityCreatedEvent
        {
            AggregateId = facility.Id,
            EventType = nameof(FacilityCreatedEvent),
            CentreId = centreId,
            Name = name,
            Type = type
        });
        
        return facility;
    }
    
    public void Update(
        string name,
        int capacity,
        string? description = null,
        List<string>? amenities = null,
        BookingRules? bookingRules = null)
    {
        Name = name;
        Capacity = capacity;
        Description = description;
        if (amenities != null) Amenities = amenities;
        if (bookingRules != null) BookingRules = bookingRules;
        MarkAsUpdated();
        
        AddDomainEvent(new FacilityUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(FacilityUpdatedEvent),
            Name = name
        });
    }
    
    public void UpdateImage(string imageUrl)
    {
        ImageUrl = imageUrl;
        MarkAsUpdated();
    }
    
    public void SetAvailable()
    {
        Status = FacilityStatus.Available;
        MarkAsUpdated();
    }
    
    public void SetOccupied()
    {
        Status = FacilityStatus.Occupied;
        MarkAsUpdated();
    }
    
    public void SetUnderMaintenance()
    {
        Status = FacilityStatus.UnderMaintenance;
        MarkAsUpdated();
    }
    
    public void Close()
    {
        Status = FacilityStatus.Closed;
        MarkAsUpdated();
    }
}

using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;

namespace SchoolConnect.Institution.Domain.Entities;

public class BookingRules
{
    public int MinDurationMinutes { get; set; }
    public int MaxDurationMinutes { get; set; }
    public int AdvanceBookingDays { get; set; }
    public bool RequiresApproval { get; set; }
    public List<string> AllowedRoles { get; set; } = [];
}

public class Facility : AggregateRoot
{
    public Guid CentreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Code { get; private set; }
    public FacilityType Type { get; private set; }
    public int Capacity { get; private set; }
    public string? Description { get; private set; }
    public List<string> Amenities { get; private set; } = [];
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
        string? code = null,
        string? description = null,
        List<string>? amenities = null,
        string? imageUrl = null,
        bool isBookable = true,
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
            Amenities = amenities ?? [],
            ImageUrl = imageUrl,
            Status = FacilityStatus.Available,
            IsBookable = isBookable,
            BookingRules = bookingRules
        };

        facility.AddDomainEvent(new FacilityCreatedEvent
        {
            AggregateId = facility.Id,
            AggregateType = nameof(Facility),
            CentreId = centreId,
            Name = name,
            Type = type
        });

        return facility;
    }

    public void Update(
        string name,
        int capacity,
        string? code = null,
        string? description = null,
        List<string>? amenities = null,
        string? imageUrl = null,
        bool? isBookable = null,
        BookingRules? bookingRules = null)
    {
        Name = name;
        Code = code;
        Capacity = capacity;
        Description = description;
        if (amenities != null) Amenities = amenities;
        ImageUrl = imageUrl;
        if (isBookable.HasValue) IsBookable = isBookable.Value;
        if (bookingRules != null) BookingRules = bookingRules;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new FacilityUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Facility),
            Name = name
        });
    }

    public void UpdateStatus(FacilityStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

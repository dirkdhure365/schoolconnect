using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.ValueObjects;
using SchoolConnect.Institution.Domain.Events;

namespace SchoolConnect.Institution.Domain.Entities;

public class Centre : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public Address Address { get; private set; } = null!;
    public ContactInfo ContactInfo { get; private set; } = null!;
    public GeoLocation? Location { get; private set; }
    public int Capacity { get; private set; }
    public CentreStatus Status { get; private set; }
    public WorkingHours? WorkingHours { get; private set; }
    public Guid? CentreAdminId { get; private set; }

    private Centre() { }

    public static Centre Create(
        Guid instituteId,
        string name,
        string code,
        Address address,
        ContactInfo contactInfo,
        int capacity,
        WorkingHours? workingHours = null,
        GeoLocation? location = null,
        Guid? centreAdminId = null)
    {
        var centre = new Centre
        {
            InstituteId = instituteId,
            Name = name,
            Code = code,
            Address = address,
            ContactInfo = contactInfo,
            Capacity = capacity,
            Status = CentreStatus.Active,
            WorkingHours = workingHours,
            Location = location,
            CentreAdminId = centreAdminId
        };

        centre.AddDomainEvent(new CentreCreatedEvent
        {
            AggregateId = centre.Id,
            AggregateType = nameof(Centre),
            InstituteId = instituteId,
            Name = name,
            Code = code
        });

        return centre;
    }

    public void Update(
        string name,
        Address address,
        ContactInfo contactInfo,
        int capacity,
        WorkingHours? workingHours = null,
        GeoLocation? location = null,
        Guid? centreAdminId = null)
    {
        Name = name;
        Address = address;
        ContactInfo = contactInfo;
        Capacity = capacity;
        WorkingHours = workingHours;
        Location = location;
        CentreAdminId = centreAdminId;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CentreUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Centre),
            Name = name
        });
    }

    public void Deactivate()
    {
        Status = CentreStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CentreDeactivatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Centre)
        });
    }

    public void Activate()
    {
        Status = CentreStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}

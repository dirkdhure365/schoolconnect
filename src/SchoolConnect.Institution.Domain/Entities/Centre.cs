using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;
using SchoolConnect.Institution.Domain.ValueObjects;

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
    public WorkingHours WorkingHours { get; private set; } = null!;
    public Guid? CentreAdminId { get; private set; }
    
    private Centre() { }
    
    public static Centre Create(
        Guid instituteId,
        string name,
        string code,
        Address address,
        ContactInfo contactInfo,
        int capacity,
        WorkingHours workingHours,
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
            WorkingHours = workingHours,
            Location = location,
            CentreAdminId = centreAdminId,
            Status = CentreStatus.Active
        };
        
        centre.AddDomainEvent(new CentreCreatedEvent
        {
            AggregateId = centre.Id,
            EventType = nameof(CentreCreatedEvent),
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
        WorkingHours workingHours,
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
        MarkAsUpdated();
        
        AddDomainEvent(new CentreUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(CentreUpdatedEvent),
            Name = name
        });
    }
    
    public void Deactivate()
    {
        Status = CentreStatus.Inactive;
        MarkAsUpdated();
        
        AddDomainEvent(new CentreDeactivatedEvent
        {
            AggregateId = Id,
            EventType = nameof(CentreDeactivatedEvent)
        });
    }
    
    public void SetUnderMaintenance()
    {
        Status = CentreStatus.UnderMaintenance;
        MarkAsUpdated();
    }
    
    public void Activate()
    {
        Status = CentreStatus.Active;
        MarkAsUpdated();
    }
}

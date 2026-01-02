using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Calendar.Domain.ValueObjects;

public class EventLocation : ValueObject
{
    public string Name { get; private set; } = string.Empty;
    public string? Address { get; private set; }
    public Guid? FacilityId { get; private set; }
    public string? FacilityName { get; private set; }
    public string? VirtualMeetingUrl { get; private set; }
    public string? VirtualMeetingProvider { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }

    private EventLocation() { }

    public static EventLocation Create(
        string name,
        string? address = null,
        Guid? facilityId = null,
        string? facilityName = null,
        string? virtualMeetingUrl = null,
        string? virtualMeetingProvider = null,
        double? latitude = null,
        double? longitude = null)
    {
        return new EventLocation
        {
            Name = name,
            Address = address,
            FacilityId = facilityId,
            FacilityName = facilityName,
            VirtualMeetingUrl = virtualMeetingUrl,
            VirtualMeetingProvider = virtualMeetingProvider,
            Latitude = latitude,
            Longitude = longitude
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Address ?? string.Empty;
        yield return FacilityId ?? Guid.Empty;
        yield return FacilityName ?? string.Empty;
        yield return VirtualMeetingUrl ?? string.Empty;
        yield return VirtualMeetingProvider ?? string.Empty;
        yield return Latitude ?? 0.0;
        yield return Longitude ?? 0.0;
    }
}

using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.ValueObjects;

public class GeoLocation : ValueObject
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    private GeoLocation() { }

    public GeoLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}

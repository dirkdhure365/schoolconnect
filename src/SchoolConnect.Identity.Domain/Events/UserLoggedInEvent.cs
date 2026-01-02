using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record UserLoggedInEvent : DomainEvent
{
    public string IpAddress { get; init; }
    public string DeviceInfo { get; init; }

    public UserLoggedInEvent(Guid aggregateId, string ipAddress, string deviceInfo, int version)
    {
        AggregateId = aggregateId;
        IpAddress = ipAddress;
        DeviceInfo = deviceInfo;
        AggregateType = "User";
        Version = version;
    }
}

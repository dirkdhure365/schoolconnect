using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record UserRegisteredEvent : DomainEvent
{
    public string Email { get; init; }
    public string UserType { get; init; }

    public UserRegisteredEvent(Guid aggregateId, string email, string userType, int version)
    {
        AggregateId = aggregateId;
        Email = email;
        UserType = userType;
        AggregateType = "User";
        Version = version;
    }
}

using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record UserVerifiedEvent : DomainEvent
{
    public string Email { get; init; }
    public string VerificationType { get; init; }

    public UserVerifiedEvent(Guid aggregateId, string email, string verificationType, int version)
    {
        AggregateId = aggregateId;
        Email = email;
        VerificationType = verificationType;
        AggregateType = "User";
        Version = version;
    }
}

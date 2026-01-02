using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record PasswordResetEvent : DomainEvent
{
    public string Email { get; init; }

    public PasswordResetEvent(Guid aggregateId, string email, int version)
    {
        AggregateId = aggregateId;
        Email = email;
        AggregateType = "User";
        Version = version;
    }
}

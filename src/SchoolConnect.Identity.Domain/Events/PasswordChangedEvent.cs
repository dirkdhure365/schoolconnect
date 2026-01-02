using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record PasswordChangedEvent : DomainEvent
{
    public PasswordChangedEvent(Guid aggregateId, int version)
    {
        AggregateId = aggregateId;
        AggregateType = "User";
        Version = version;
    }
}

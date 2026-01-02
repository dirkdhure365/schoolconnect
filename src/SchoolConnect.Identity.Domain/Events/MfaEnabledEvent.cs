using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record MfaEnabledEvent : DomainEvent
{
    public MfaEnabledEvent(Guid aggregateId, int version)
    {
        AggregateId = aggregateId;
        AggregateType = "User";
        Version = version;
    }
}
